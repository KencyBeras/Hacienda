﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudHacienda.Filtros;

namespace CrudHacienda.Controllers
{
    [Acceder]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

       
    }
}