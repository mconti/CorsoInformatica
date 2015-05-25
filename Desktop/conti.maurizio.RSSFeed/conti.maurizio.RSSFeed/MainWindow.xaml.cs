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

namespace conti.maurizio.RSSFeed
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

        private void ApriGiornale_click(object sender, RoutedEventArgs e)
        {
            try
            {
                XElement RSS = XElement.Load("http://www.ilgiornale.it/feed.xml");

                var res = from riga in RSS.Element("channel").Elements("item")
                          select new Feed 
                          { 
                              Titolo = riga.Element("title").Value,
                              Descrizione = riga.Element("description").Value,
                              Link = riga.Element("link").Value 
                          };;

                dgElenco.ItemsSource = res;
            }
            catch { }
        }

        private void ApriArticolo_click(object sender, RoutedEventArgs e)
        {
            var feed = dgElenco.SelectedItem as Feed;
            
            if( feed != null)
            {
                Monitor m = new Monitor();
                m.Articolo = feed;
                m.ShowDialog();
   
            }
        }
    }

    public class Feed
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string Link { get; set; }
    }
}
