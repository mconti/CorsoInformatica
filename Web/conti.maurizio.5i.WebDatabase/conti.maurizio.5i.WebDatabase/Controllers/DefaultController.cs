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
            DAL dal = new DAL("Database1.accdb");
            DataTable table = dal.Getdata("select * from auto");
            return View(table);
        }
    }
}