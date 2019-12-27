using LukaszKijak.Models;
using LukaszKijak.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Controllers
{
    public class Explorer : Controller
    {
        private ICheckRootFolder checkRootFolder;

        public Explorer(ICheckRootFolder checkFld)
        {
            checkRootFolder = checkFld;
        }

        [HttpPost]
        public ViewResult Index(string newPath)
        {
            var path = Directory.GetCurrentDirectory();
            var list = checkRootFolder.GetFolderContent(path);
            return View(list);
        }

    }
}
