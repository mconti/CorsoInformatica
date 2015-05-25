using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO.Ports;
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

namespace WPFTestSeriale
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] PortNames { get; set; }
        ObservableCollection<double> Dati;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            Dati = new ObservableCollection<double>();
            lvDati.ItemsSource = Dati;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += timer_Tick;
            timer.Start();

            PortNames = SerialPort.GetPortNames();
            cboxPortNames.ItemsSource = new List<string>(PortNames);

            if (PortNames.Count() > 1)
                cboxPortNames.SelectedIndex = 1;

            int a = com1.BytesToRead;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            GetDati();
        }


        SerialPort _com1 = null;

        SerialPort com1
        {
            get
            {
                try
                {
                    if (_com1 == null)
                        _com1 = new SerialPort(cboxPortNames.SelectedValue.ToString(), 9600, Parity.None, 8, StopBits.One);

                    if (!_com1.IsOpen)
                    {
                        //_com1.DataReceived += _com1_DataReceived;
                        //_com1.RtsEnable = true;
                        _com1.DtrEnable = true;
                        _com1.NewLine = "\r\n";

                        _com1.Open();
                        _com1.DiscardInBuffer();
                        _com1.ReadTimeout = 1000;
                    }
                }
                catch { }

                return _com1;
            }
        }

        #region Nel caso in cui si voglia leggere i dati in modalità async
        void _com1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort p = sender as SerialPort;
            int quantiByte = p.BytesToRead;
            if (quantiByte > 0)
            {
                //Console.Beep(4000, 50);
                char[] buf = new char[quantiByte];
                int idx = 1;

                while (idx < quantiByte)
                    buf[quantiByte - idx++] = (char)com1.ReadChar();

                com1.DiscardInBuffer();
            }
        }
#endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetDati();
        }

        private void GetDati()
        {
            try
            {
                string s = com1.ReadLine();
                Dati.Add(Convert.ToDouble(s, CultureInfo.InvariantCulture));

                lvDati.ItemsSource = null;
                lvDati.ItemsSource = Dati.Reverse();

            }
            catch (Exception errore) { MessageBox.Show(errore.Message); }
        }

    }
}
