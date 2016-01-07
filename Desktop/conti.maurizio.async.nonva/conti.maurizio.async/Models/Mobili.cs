using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conti.maurizio.async
{
    public class Mobili : ObservableCollection<Mobile>
    {
        public Mobili()
        { }

        public Mobili(string NomeFile)
        {
            StreamReader sr = new StreamReader(NomeFile);
            sr.ReadLine();
			
            while (!sr.EndOfStream)
            {
                Add(new Mobile(sr.ReadLine()));
            }
        }

    }
}
