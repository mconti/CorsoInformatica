using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace MvcApplication1.Controllers
{
    //[Authorize]
    public class AutoController : ApiController
    {
        DAL dal = new DAL();
        List<Auto> automobili = new List<Auto>();
        string errore = "";

        public AutoController() 
        {
            try
            {
                DataTable auto = dal.Getdata("select * from Auto");

                automobili = (from DataRow row in auto.Rows
                              select new Auto
                              {
                                  ID = Convert.ToInt32(row["Id"]),
                                  Marca = row["Marca"].ToString().Trim(),
                                  Modello = row["Modello"].ToString().Trim()
                              }).ToList();
            }
            catch (Exception Err) { errore = Err.Message; }
        }

        // GET api/auto
        //[AllowAnonymous]
        public IEnumerable<Auto> Get()
        {
            return automobili;
        }

        // GET api/auto/5
        public Auto Get(int id)
        {
            return (from a in automobili
                    where a.ID == id
                    select a).FirstOrDefault();
        }

        // POST api/auto
        public int Post(Auto value)
        {
            string query = "insert into Auto(Marca, Modello) values( '" + value.Marca + "', '" + value.Modello + "')";
            return dal.Insert(query);
        }

        // PUT api/auto/5
        public void Put(int id, Auto value)
        {
            String query = String.Format("update Auto set Marca='{0}', Modello='{1}' where id={2}",
                value.Marca, value.Modello, value.ID);

            dal.Insert(query);
        }

        // DELETE api/auto/5
        public void Delete(int id)
        {
            string query = "delete from Auto where ID=" + id;
            dal.Insert(query);
        }
    }
}
