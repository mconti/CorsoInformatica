using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step3
{
    class Persona
    {
        public Persona()
        { }

        public int ID { get; set; }
        public string Nome { get; set; }

        public virtual double Reddito 
        {
            get { return 0; }
        }
    }

    class Dipendente : Persona
    {
        public Dipendente()
            : base()
        {
            StipendioMensile = 100;
        }

        public double StipendioMensile { get; set; }

        public override double Reddito
        {
            get
            {
                return StipendioMensile * 12;
            }
        }
    }

    class Operaio : Dipendente
    {
        public double Stipendio { get; set; }
    }

    class Persone : List<Persona>
    {
        //
        // Note sull'architettura
        // Elementi non esiste più.
        //
        // Persone è un List<Persona> migliore, usa la classe base per specializzarla.
        // L'ereditarierà è migliore della composizione perchè aiuta il progettista a rispettare anche il principio di singola responsabilità.
        //
        
        public Persone()
        {}

        public Persone(string NomeFile)
            : this()
        {
            StreamReader rd = new StreamReader(NomeFile);

            string line = rd.ReadLine();
            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] campi = line.Split(new char[] { ';', ',', '.' });

                int id = Convert.ToInt32(campi[0]);

                // UpCasting!
                // Inserire dei Dipendente in una List<Persona> è lecito.
                // Un "Dipendente" infatti ha sicuramente tutto quello che ha un "Persona"
                // Qui entra in gioco il polimorfismo che chiama i metodi dichiarati virtual, nel modo corretto.
                if( id > 5)
                    Add(new Persona { ID = id, Nome = campi[1] });
                else
                    Add(new Dipendente { ID = id, Nome = campi[1] });

            }
        }

    }
}
