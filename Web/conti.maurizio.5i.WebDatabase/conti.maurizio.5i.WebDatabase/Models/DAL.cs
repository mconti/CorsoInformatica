using System;
using System.Collections.Generic;
using System.Data;
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
        //string NomeFileDb = @"AttachDbFileName=|DataDirectory|\Database1.accdb;";
        //string NomeFileDb = @"";
        //string NomeFileDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database1.accdb;";
        
        // e il campo security
        //string tipoSicurezza = @"Integrated Security=true;";
        //string tipoSicurezza = @"User Id=automobili;Password=jJ86@u)5k[6e;";
        string tipoSicurezza = @";Persist Security Info=False";

        //SqlConnection cn { get; set; }
        System.Data.OleDb.OleDbConnection cn { get; set; }

        // Nome del file .MDB
        public string DBFileName { get; set; }

        // Connection string
        public string ConnectionString { get; set; }

        // La stringa di connessione
        public DAL() { }

        public DAL(string dbFileName)
        {
            string NomeFileDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\" + dbFileName;
            ConnectionString = NomeServer + NomeFileDb + tipoSicurezza;
            //cn = new SqlConnection(ConnectionString);
            cn = new System.Data.OleDb.OleDbConnection(ConnectionString);
        }

        public DataTable Getdata(string query)
        {
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(query, cn);
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            return tbl;
        }

        //public int Insert(string query)
        //{
        //    int retVal = 0;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(query, cn);
        //        cmd.Connection.Open();
        //        retVal = cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //    catch { }
        //    return retVal;
        //}
    }
}