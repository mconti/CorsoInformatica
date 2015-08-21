using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Step1
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Persona> persone = new List<Persona>();

        public MainWindow()
        {
            InitializeComponent();

            Persona p1 = new Persona();
            
            // Sollevano Exception
            //p1.Data = 1000;
            //p1.setData(1000);

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader rd = new StreamReader("Persone.csv");
                Persona p;     

                string line = rd.ReadLine();
                while ( !rd.EndOfStream )
                {
                    line = rd.ReadLine();
                    string[] campi = line.Split(new char[] { ';', ',', '.' });

                    // p = new Persona { ID = Convert.ToInt32(campi[0]), Nome = campi[1] };
                    persone.Add(new Persona { ID = Convert.ToInt32(campi[0]), Nome = campi[1] });

                }

            }
            catch { }
        }
    }

    class Persona
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        // Field data
        private int _data;
        
        // Property appoggiata al field data
        public int Data
        {
            get 
            { 
                return _data; 
            }

            set 
            {
                if (value > 100)
                    throw new Exception("EhEHEH!!!!");

                _data = value; 
            }
        }

        public int getData()
        {
            return _data;
        }

        public void setData( int val )
        {
            if (val > 100)
                throw new Exception("EhEHEH!!!!");

            _data = val; 
        }

        public int GetDataPlus()
        {
            return _data+1;
        }
    }
}
