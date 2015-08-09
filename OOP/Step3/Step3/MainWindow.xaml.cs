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

namespace Step3
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Persone p { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                p = new Persone("Persone.csv");

            }
            catch
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // p non ha un Elementi
            //dgDati.ItemsSource = p.Elementi;

            // p è un elementi
            dgDati.ItemsSource = p;

        }
    }
}
