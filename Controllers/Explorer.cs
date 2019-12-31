using LukaszKijak.Models;
using LukaszKijak.Service;
using LukaszKijak.Service.SortList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LukaszKijak.Controllers
{
    public class Explorer : Controller
    {
        private IShowFolder showFolder;

        public Explorer(IShowFolder checkFld)
        {
            showFolder = checkFld;
        }

        public ViewResult Index(string path, string sort, string navigation)
        {
            List<ViewModel> list = null;
            string newPath = null;

            if (path == "start" && navigation == null)
            {
                AddToNewPath(path);
                newPath = GetMainPath();
                list = showFolder.GetFolderContent(newPath, sort);
                ViewBag.MyNewPath = GetNewPath();
                ViewBag.Root = "rootFolder";
                return View(list);
            }
            else if(path != "start" && navigation == null)
            {
                AddToNewPath(path);
                newPath = GetNewPath();
                list = showFolder.GetFolderContent(newPath, sort);
                ViewBag.MyNewPath = GetNewPath();
                ViewBag.Root = "";
                return View(list);
            }
            else if (path == null && navigation == "up")
            {
                AddToNewPath(navigation);
                newPath = GetNewPath();
                list = showFolder.GetFolderContent(newPath, sort);
                ViewBag.MyNewPath = GetNewPath();
                if (GetNewPath() == GetMainPath())
                {
                    ViewBag.Root = "rootFolder";
                }
                return View(list);
            }
            list = showFolder.GetFolderContent(GetMainPath(), sort);
            ViewBag.MyNewPath = GetMainPath();
            return View(list);
        }

        public IActionResult Rename(string oldName)
        {
            EditModel model = new EditModel
            {
                OldName = oldName,
                NewName = oldName
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Rename(EditModel model)
        {
            var filePath = GetNewPath();
            string oldName = filePath + "/" + model.OldName;
            string newName = filePath + "/" + model.NewName;

            try
            {
                System.IO.File.Move(oldName, newName);
                return RedirectToAction("Confirm");
            }
            catch (FileNotFoundException)
            {
                Directory.Move(oldName, newName);
                return RedirectToAction("Confirm");
            }
        }
        public ViewResult Confirm() => View();


        //**************************** Set and get current path
        public string GetMainPath()
        {
            string path = HttpContext.Session.GetString("mainPath");
            var newPath = path.Replace("\\", "/");
            return newPath;
        }
        public string GetNewPath()
        {
            string newPath = HttpContext.Session.GetString("newPath");
            return newPath;
        }
        public void AddToNewPath(string path)
        {
            string newPath;
            if (path == "start")
            {
                newPath = GetMainPath();
                HttpContext.Session.SetString("newPath", newPath);
            }
            else if (path == "up")
            {
                var cutPath = GetNewPath();
                int counter = cutPath.LastIndexOf("/");
                newPath = cutPath.Substring(0, counter);
                HttpContext.Session.SetString("newPath", newPath);
            }
            else if (path != null)
            {
                newPath = GetNewPath() + "/" + path;
                HttpContext.Session.SetString("newPath", newPath);
            }
        }

    }
}
