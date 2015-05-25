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
using System.Windows.Shapes;

namespace conti.maurizio.RSSFeed
{
    /// <summary>
    /// Logica di interazione per Monitor.xaml
    /// </summary>
    public partial class Monitor : Window
    {
        public Feed Articolo { get; set; }
        public Monitor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            browser.NavigateToString(System.Net.WebUtility.HtmlDecode(Articolo.Descrizione));
        }
    }
}
