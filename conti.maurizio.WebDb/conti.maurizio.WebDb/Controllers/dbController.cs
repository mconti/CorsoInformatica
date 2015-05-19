using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace conti.maurizio.WebDb.Controllers
{
    public class dbController : Controller
    {
        // GET: db
        public ActionResult Index()
        {
            string connStr = ConfigurationManager.ConnectionStrings["cnLibri"].ConnectionString;

            DataTable retVal = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Categories", new SqlConnection(connStr));

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(retVal);

            return View(retVal);
        }

    }
}