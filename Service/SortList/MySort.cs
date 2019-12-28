using LukaszKijak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Service.SortList
{
    public class MySort : IMySort
    {
        private List<ViewModel> sortedList { get; set; }

        public List<ViewModel> SortList(List<ViewModel> unSortedList,
            string sortBy)
        {
            if (sortBy == "Type")
            {
                sortedList = unSortedList.OrderBy(o => o.Type).ToList();
                return sortedList;
            }
            else if (sortBy == "Name")
            {
                sortedList = unSortedList.OrderBy(o => o.Name).ToList();
                return sortedList;
            }
            else if (sortBy == "Modification")
            {
                sortedList = unSortedList.OrderBy(o => o.ModificationDate).ToList();
                return sortedList;
            }
            else if (sortBy == "Size")
            {
                sortedList = unSortedList.OrderBy(o => o.Size).ToList();
                return sortedList;
            }
            else if (sortBy == "Attribute")
            {
                sortedList = unSortedList.OrderBy(o => o.Attribute).ToList();
                return sortedList;
            }
            else if (sortBy == "TypeDescending")
            {
                sortedList = unSortedList.OrderByDescending(o => o.Type).ToList();
                return sortedList;
            }
            if (sortBy == "NameDescending")
            {
                sortedList = unSortedList.OrderByDescending(o => o.Name).ToList();
                return sortedList;
            }
            else if (sortBy == "ModificationDescending")
            {
                sortedList = unSortedList.OrderByDescending(o => o.ModificationDate).ToList();
                return sortedList;
            }
            else if (sortBy == "SizeDescending")
            {
                sortedList = unSortedList.OrderByDescending(o => o.Size).ToList();
                return sortedList;
            }
            else if (sortBy == "AttributeDescending")
            {
                sortedList = unSortedList.OrderByDescending(o => o.Attribute).ToList();
                return sortedList;
            }
            return unSortedList;
        }

    }
}
