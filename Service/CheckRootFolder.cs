using LukaszKijak.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Service
{
    public class CheckRootFolder : ICheckRootFolder
    {
        private IConfiguration configuration;

        public CheckRootFolder(IConfiguration config)
        {
            configuration = config;
        }

        public List<ViewModel> GetFolderContent(string path)
        {
            List<ViewModel> contentList = new List<ViewModel>();
            
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
                var size = info.Length;
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
            return contentList;
        }

        
    }
}
