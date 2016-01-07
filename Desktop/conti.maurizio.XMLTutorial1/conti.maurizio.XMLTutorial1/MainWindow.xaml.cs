using System;
using System.Collections.Generic;
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
using System.Xml.Linq;

namespace conti.maurizio.XMLTutorial1
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

        private void btnApriClick(object sender, RoutedEventArgs e)
        {
            try
            {

                XElement dati = XElement.Load("Dati.xml");

                // Tutti gli elementi contentuti in root
                // Che siano "Libri" oppure no
                IEnumerable<XElement> tuttiGliElementi = dati.Elements();

                // Tutti gli elementi "Libri" contentuti in root
                // Solo gli elementi "Libri" (nel file Dati.xml ne abbiamo uno ma è comunque un vettore)
                IEnumerable<XElement> tuttiIlibri = dati.Elements("Libri");

                // Il primo elemento "Libri" contenuto in root
                XElement libri = dati.Elements("Libri").First();

                // Tutti i "Libro" contenuti in libri
                IEnumerable<XElement> tuttiIlibro = dati.Elements("Libri").First().Elements("Libro");

                // Il primo elemento "Libro" contenuto in libri
                XElement primoLibro = dati.Elements("Libri").First().Elements("Libro").First();

                // Stampa tutto l'elemento (<Libro Titolo="Harry Potter" Autore="J. K. Rowling">FotoHP.jpg</Libro>)
                Console.WriteLine(primoLibro);

                // Stampa tutto l'attributo (Titolo="Harry Potter")
                Console.WriteLine(primoLibro.Attribute("Titolo"));

                // Stampa solo il valore (Harry Potter)
                Console.WriteLine(primoLibro.Attribute("Titolo").Value);

                // Stampa il valore contenuto tra il tag di apertura e il tag di chiusura (FotoHP.jpg)
                Console.WriteLine(primoLibro.Value);

                dgDati.ItemsSource = from l in dati.Elements("Libri").First().Elements("Libro")
                                     select new Libro(l);

            }
            catch (Exception Errore)
            {
                MessageBox.Show(Errore.Message);
            }
        }
    }
}
