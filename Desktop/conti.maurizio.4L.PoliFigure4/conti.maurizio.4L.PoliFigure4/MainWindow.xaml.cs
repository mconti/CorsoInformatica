using conti.maurizio._4L.PoliFigure4.Models;
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

namespace conti.maurizio._4L.PoliFigure4
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Metto tutto sotto try catch.
            // Il file potrebbe non esistere
            try
            {
                // Tutte le ficure che carico da disco le metto qui
                List<Figura> figure = new List<Figura>();
                
                // Apro il file e scarto la prima riga
                StreamReader fileIn = new StreamReader("Figure.csv");
                fileIn.ReadLine();

                // Leggo tutte le righe, una alla volta.
                do
                {
                    // Il procedimento è questo: 
                    // Leggo la riga e con Split() la divido nelle sue parti
                    string riga = fileIn.ReadLine();
                    string[] campi = riga.Split(';');

                    // Guardo al primo campo e decido quale oggetto creare.
                    // Poi lo creo e lo aggiungo al contenitore delle figure.
                    if (campi[0] == "Quadrato")
                        figure.Add(new Quadrato(riga));
                    else if (campi[0] == "Rettangolo")
                        figure.Add(new Rettangolo(riga));
                    else if (campi[0] == "Triangolo")
                        figure.Add(new Triangolo(riga));

                    // Nota: Ogni oggetto è dotato di un costruttore particolare 
                    // che gli consente di crearsi dalla String che gli viene passata
                }
                while (!fileIn.EndOfStream); // Fino alla fine del file

                // Le figure lette vengono visualizzate su un componente ListView
                lvFigure.ItemsSource = figure;
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
    }
}
