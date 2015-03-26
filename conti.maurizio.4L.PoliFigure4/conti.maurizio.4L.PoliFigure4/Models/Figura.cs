using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conti.maurizio._4L.PoliFigure4.Models
{
    public class Figura
    {
        public double Misura1 { get; set; }
        public double Misura2 { get; set; }
        public double Misura3 { get; set; }
        
        public Figura() 
        {
            Misura1 = 0;
            Misura2 = 0;
            Misura3 = 0;
        }
        
        public Figura(string riga)
        {
            string[] campi = riga.Split(';');
            Misura1 = Convert.ToDouble(campi[1]);
            Misura2 = Convert.ToDouble(campi[2]);
            Misura3 = Convert.ToDouble(campi[3]);
        }
        public override string ToString() { return "Misura1: " + Misura1 + ", Misura2: " + Misura2 + " Misura3: " + Misura3; }

    }

    public class Quadrato : Figura
    {
        public double Lato { get { return Misura1; } }
        
        public Quadrato(string riga) : base(riga) { }
        public override string ToString() { return "Quadrato. Lato: " + Lato; }
    }
    public class Rettangolo : Figura
    {
        public double Base { get { return Misura1; } }
        public double Altezza { get { return Misura2; } }

        public Rettangolo(string riga) : base(riga) { }
        public override string ToString() { return "Rettangolo. Base:" + Base + ", Altezza:" + Altezza; }
    }

    public class Triangolo : Figura
    {
        public double Lato1 { get { return Misura1; } }
        public double Lato2 { get { return Misura2; } }
        public double Lato3 { get { return Misura3; } }

        public Triangolo(string riga) : base(riga) { }
        public override string ToString() { return "Triangolo. Lato1:" + Lato1 + ", Lato2:" + Lato2 + "  Lato3:" + Lato3; }
    }

}
