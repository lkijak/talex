﻿using LukaszKijak.Models;
using Microsoft.AspNetCore.Http;
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
            //ViewBag.StartPath = Directory.GetCurrentDirectory();
            string mainPath = Directory.GetCurrentDirectory();
            HttpContext.Session.SetString("mainPath", mainPath);
            return View();
        }


    }
}
