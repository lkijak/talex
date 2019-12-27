using LukaszKijak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Service
{
    public interface ICheckRootFolder
    {
        List<ViewModel> GetFolderContent(string path);



    }
}
