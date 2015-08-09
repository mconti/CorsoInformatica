using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Step2
{
    class Persona
    {
        public int ID { get; set; }
        public string Nome { get; set; }
    }

    class Persone
    {
        // Note sull'architettura
        // Elementi, ha una relazione di tipo "Composizione" con Persone perchè:
        // - La responsabilità per la creazione di Elementi è di Persone
        // - Se viene distrutto Persone, anche Elementi viene distrutto
        // - Elementi può essere visto solo da dentro Persone, non esistono altre classi che lo usano.
        //
        // Quando abbiamo una relazione di composizione, l'aspetto sul quale stiamo ragionando è legato alla domanda "ha un?"
        // Se la classe che andiamo a modellare invece ci sembra sempre di più vicino al concetto di "è un", allora l'ereditarietà è lo strumneto più giusto da usare.
        //

        public List<Persona> Elementi { get; private set; }

        public Persone()
        {
            Elementi = new List<Persona>();
        }

        public Persone(string NomeFile)
            : this()
        {
            StreamReader rd = new StreamReader("Persone.csv");

            string line = rd.ReadLine();
            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] campi = line.Split(new char[] { ';', ',', '.' });

                Persona p = new Persona { ID = Convert.ToInt32(campi[0]), Nome = campi[1] };
                Elementi.Add(p);

            }
        }

    }
}
