using LukaszKijak.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LukaszKijak.Controllers
{
    public class Home : Controller
    {
        public ViewResult Index()
        {
            ViewModel model = new ViewModel();
            model.Path = Directory.GetCurrentDirectory();
            return View(model);
        }


    }
}
