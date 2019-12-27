using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Models
{
    public class ViewModel
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime ModificationDate { get; set; }
        public double Size { get; set; }
        public string Attribute { get; set; }
        public string Path { get; set; }
    }
}
