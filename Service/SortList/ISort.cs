using LukaszKijak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Service.SortList
{
    public interface IMySort
    {
        List<ViewModel> SortList(List<ViewModel> unSortedList,
            string sortBy);


    }
}
