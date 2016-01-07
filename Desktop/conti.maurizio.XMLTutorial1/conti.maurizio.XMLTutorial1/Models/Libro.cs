using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace conti.maurizio.XMLTutorial1
{
    class Libro
    {
        public string Titolo { get; set; }
        public string Autore { get; set; }

        public Libro(XElement e)
        {
            Titolo = e.Attribute("Titolo").Value;
            Autore = e.Attribute("Autore").Value;
        }
    }
}
