using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace conti.maurizio._5i.WebDatabase
{
    public class DAL
    {
        // Nella versione minima abbiamo il campo "Server"
        //string NomeServer = @"Server=(localdb)\v11.0;";
        //string NomeServer = @"Server=.\SQLExpress;";
        //string NomeServer = @"Server=mssql1.gear.host;Database=Automobili;";
        string NomeServer = @"";

        //... il campo Filename
        //string NomeFileDb = @"AttachDbFileName=|DataDirectory|\Database1.MDF;Database=Database1";
        //string NomeFileDb = @"AttachDbFileName=|DataDirectory|\Database1.MDF;";
        string NomeFileDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database1.accdb;";
        
        // e il campo security
        string tipoSicurezza = @"Persist Security Info=False";

        //SqlConnection cn { get; set; }
        OleDbConnection cn { get; set; }

        // La stringa di connessione
        public DAL()
        {
            string stringaConnessione = NomeServer + NomeFileDb + tipoSicurezza;
            cn = new System.Data.OleDb.OleDbConnection(stringaConnessione);
        }

        public DataTable Getdata(string query)
        {
            OleDbCommand cmd = new OleDbCommand(query, cn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            return tbl;
        }

        public int Insert(string query)
        {
            int retVal = 0;
            try
            {
                OleDbCommand cmd = new OleDbCommand(query, cn);
                cmd.Connection.Open();
                retVal = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch { }
            return retVal;
        }
    }
}