using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace conti.maurizio._5i.WebDatabase.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Query1(string Query, string SearchString2)
        {
            DAL dal = new DAL("Database1.accdb");
            DataTable table = dal.Getdata(Query);
            return View(table);

            //return View("~/Views/Default/Index.cshtml", table);
        }
    }
}