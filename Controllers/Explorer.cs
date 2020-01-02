using LukaszKijak.Models;
using LukaszKijak.Service;
using LukaszKijak.Service.SortList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                if (GetNewPath() == GetMainPath())
                {
                    ViewBag.Root = "rootFolder";
                }
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
                return View("Confirm");
            }
            catch (FileNotFoundException)
            {
                Directory.Move(oldName, newName);
                return View("Confirm");
            }
        }

        public IActionResult OpenFile(string name)
        {
            if (name != null)
            {
                var directory = GetNewPath();

                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = name;
                process.StartInfo.WorkingDirectory = directory;
                process.Start();
                
                return RedirectToAction("Index");
            }
            return View("ErrorOpen");
            
        }

        public IActionResult DownloadFile(string name)
        {
            ContentType content = new ContentType();
            var fileName = GetNewPath() + "/" + name;
            var type = content.GetContentType(fileName);

            MemoryStream memory = new MemoryStream();
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, type, Path.GetFileName(fileName));
        }

        public ActionResult ConfirmDelete(string name)
        {
            TempData["DeleteItemName"] = name;
            return View();
        }

        public IActionResult Delete()
        {
            var name = TempData["DeleteItemName"];
            var itemName = GetNewPath() + "/" + name;
            FileAttributes attr = System.IO.File.GetAttributes(itemName);
            if (attr != FileAttributes.Directory)
            {
                System.IO.File.Delete(itemName);
                return View("ConfirmationDelete");
            }

            if (Directory.GetFileSystemEntries(itemName).Length == 0)
            {
                Directory.Delete(itemName);
                return View("ConfirmationDelete");
            }
            return View("ErrorDelete");
        }


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
