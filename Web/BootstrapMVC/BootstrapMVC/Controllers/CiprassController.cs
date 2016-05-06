using BootstrapMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapMVC.Controllers
{
    public class CiprassController : Controller
    {
        // GET: Ciprass
        public ActionResult Index()
        {
            Libro l = new Libro(2015) { Nome = "Pamela", Cognome = "Sanchini", Anno = 2016 };
            Libro l1 = new Libro { Nome = "Pamela", Cognome = "Sanchini" };

            return View();
        }
    }
}