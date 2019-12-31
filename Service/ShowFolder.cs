using LukaszKijak.Models;
using LukaszKijak.Service.SortList;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Service
{
    public class ShowFolder : IShowFolder
    {
        public List<ViewModel> GetFolderContent(string path, string sortBy)
        {
            List<ViewModel> contentList = new List<ViewModel>();
            List<ViewModel> sortedList = null;
            MySort mySort = new MySort();

            var folders = Directory.EnumerateDirectories(path);
            var files = Directory.EnumerateFiles(path);

            foreach (var item in folders)
            {
                DirectoryInfo info = new DirectoryInfo(item);
                var name = info.Name;
                var date = info.LastWriteTime;
                var attribute = info.Attributes.ToString();
                contentList.Add(new ViewModel
                {
                    Type = "folder",
                    Name = name,
                    ModificationDate = date,
                    Attribute = attribute
                });
            }

            foreach (var item in files)
            {
                FileInfo info = new FileInfo(item);
                var name = info.Name;
                var date = info.LastWriteTime;
                var size = ((double)info.Length / 1000);
                var attribute = info.Attributes.ToString();
                contentList.Add(new ViewModel
                {
                    Type = "files",
                    Name = name,
                    ModificationDate = date,
                    Size = size,
                    Attribute = attribute
                });
            }
            sortedList = mySort.SortList(contentList, sortBy);

            return sortedList;
        }

        
    }
}
