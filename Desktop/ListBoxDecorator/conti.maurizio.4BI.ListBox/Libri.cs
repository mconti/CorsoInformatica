using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace conti.maurizio._4BI.ListBox
{
    public enum EdizioneLibro { Normale, BestSeller, InOfferta }

    public class Libro
    {
        public Libro()
        {}

        public string Titolo { get; set; }
        public string Autore { get; set; }
        public EdizioneLibro Edizione { get; set; }
    }

}
