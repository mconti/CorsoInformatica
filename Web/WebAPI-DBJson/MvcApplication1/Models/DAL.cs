using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MvcApplication1.Models
{
    //Per Gear Host Includere:
    // System.Web.Http.WebHost
    // System.Web.Http
    // System.Net.Http.Formatting
    
    // Esempio Contoso University
    // http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application

    // Esempio Angular CRUD
    //http://www.codeproject.com/Articles/832288/CRUD-with-SPA-ASP-NET-Web-API-and-Angular-js


    class DAL
    {
        // Nella versione minima abbiamo il campo "Server"
        //string NomeServer = @"Server=(localdb)\v11.0;";
        //string NomeServer = @"Server=.\SQLExpress;";
        string NomeServer = @"Server=mssql1.gear.host;Database=Automobili;";
        
        //... il campo Filename
        //string NomeFileDb = @"AttachDbFileName=|DataDirectory|\Database1.MDF;Database=Database1";
        //string NomeFileDb = @"AttachDbFileName=|DataDirectory|\Database1.MDF;";
        string NomeFileDb = @"";
        
        // e il campo security
        //string tipoSicurezza = @"Integrated Security=true;";
        string tipoSicurezza = @"User Id=automobili;Password=jJ86@u)5k[6e;";
        
        SqlConnection cn { get; set; }
        
        // La stringa di connessione
        public DAL()
        {
            string stringaConnesione = NomeServer + NomeFileDb + tipoSicurezza;
            cn = new SqlConnection(stringaConnesione);
        }

        public DataTable Getdata(string query)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            return tbl;
        }
        public int Insert(string query)
        {
            int retVal = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Connection.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch { }
            return retVal;
        }
    }
}
