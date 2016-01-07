using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conti.maurizio.async
{
    public class Mobile
    {
        public string Marca { get; set; }
        public string Modello { get; set; }
        public string Foto { get; set; }
        public int Prezzo { get; set; }
        public string Tipo { get; set; }

        public Mobile() { }

        public Mobile(string riga)
        {
            string[] campi = riga.Split(';');

            Marca = campi[0];
            Modello = campi[1];
            Foto = campi[2];
            Prezzo = Convert.ToInt32(campi[3]);
            Tipo = campi[4];
        }

    }
}
