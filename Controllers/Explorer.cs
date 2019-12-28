using LukaszKijak.Models;
using LukaszKijak.Service;
using LukaszKijak.Service.SortList;
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
        private IMySort mySort;

        public Explorer(ICheckRootFolder checkFld,
            IMySort srt)
        {
            checkRootFolder = checkFld;
            mySort = srt;
        }

        public ViewResult Index(string path, string sort)
        {

            List<ViewModel> list = null;
            if (path != null)
            {
                list = checkRootFolder.GetFolderContent(path);
                return View(list);
            }
            path = Directory.GetCurrentDirectory();
            list = checkRootFolder.GetFolderContent(path);
            var sortedList = mySort.SortList(list, sort);
            return View(sortedList);
        }



    }
}
