using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapMVC.Models
{
    class Libro
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Anno { get; set; }

        public Libro()
        {
            Anno = DateTime.Now.Year;
        }

        public Libro( int val )
        {
            Anno = val;
        }
    }
}
