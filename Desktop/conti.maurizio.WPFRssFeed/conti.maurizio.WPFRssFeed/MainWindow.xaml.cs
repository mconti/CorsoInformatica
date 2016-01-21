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
using System.Windows.Threading;

namespace conti.maurizio.WPFRssFeed
{
    public partial class MainWindow : Window
    {
        DispatcherTimer timer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            btnUpdate_Click(sender, null);
            timer.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //string url = "https://news.google.it/?output=rss";
            string url = "http://www.ilmattino.it/rss/home.xml";

            btnUpdate.IsEnabled = false;
            txtUpdate.Text = "updating...";

            Sorgente s = new Sorgente(url);
            lvItems.ItemsSource = s.Items;

            txtUpdate.Text = url + " - " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            btnUpdate.IsEnabled = true;
        }
    }
}

