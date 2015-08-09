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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StreamReader rd = new StreamReader("Persone.csv");

                string line = rd.ReadLine();
                while ( !rd.EndOfStream )
                {
                    line = rd.ReadLine();
                    string[] campi = line.Split(new char[] { ';', ',', '.' });

                    Persona p = new Persona { ID = Convert.ToInt32(campi[0]), Nome = campi[1] };
                    persone.Add(p);

                }

            }
            catch { }
        }
    }

    class Persona
    {
        public int ID { get; set; }
        public string Nome { get; set; }
    }
}
