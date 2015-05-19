using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace conti.maurizio.wpfTestSeriale
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort _com1 = null;
        SerialPort com1 { 
            get 
            {
                try
                {
                    if (_com1 == null)
                        _com1 = new SerialPort(cboxPortNames.SelectedValue.ToString(), 57600, Parity.None, 8, StopBits.One);

                    if (!_com1.IsOpen)
                    {
                        _com1.DataReceived += _com1_DataReceived;
                        //_com1.RtsEnable = true;
                        //_com1.DtrEnable = true;
                        _com1.NewLine = "\r\n";
                        
                        _com1.Open();
                        _com1.DiscardInBuffer();
                        com1.ReadTimeout = 1000;
                    }
                }
                catch { }

                return _com1;
            } 
        }

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
                    buf[quantiByte - idx++] = (char) com1.ReadChar();

                com1.DiscardInBuffer();
            }
        }

        string[] PortNames { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PortNames = SerialPort.GetPortNames();
            cboxPortNames.ItemsSource = new List<string>(PortNames);

            if (PortNames.Count() > 1)
                cboxPortNames.SelectedIndex = 1;
        }

        private void getA0_Click(object sender, RoutedEventArgs e)
        {
            // Analog Read pin=0, valore non importa -> 0
            string cmd = String.Format("AR.0.0!");

            string[] valori = SendCommand(cmd);
            pbPotenziometro.Value = Convert.ToInt32(valori[1]);
        }

        private void setD6_Click(object sender, RoutedEventArgs e)
        {
            // Analog Write pin=6, value=(preso dallo slider)
            int valore = Convert.ToInt32(slServo.Value);
            string cmd = String.Format("AW.6.{0}!", valore);

            SendCommand(cmd);
        }
        private void getD2_Click(object sender, RoutedEventArgs e)
        {
            // Digital Read pin=2, valore non importa -> 0
            string cmd = String.Format("DR.2.0!");

            string[] valori = SendCommand(cmd);
            int val = Convert.ToInt32(valori[1]);

            if( val == 0 )
                cvPulsante.Background = Brushes.Gray;
            else
                cvPulsante.Background = Brushes.Green;
        }
        private void getD3_Click(object sender, RoutedEventArgs e)
        {
            // Digital Read pin=3, valore non importa -> 0
            string cmd = String.Format("DR.3.0!");

            string[] valori = SendCommand(cmd);
            int val = Convert.ToInt32(valori[1]);

            if (val == 0)
                cvInterruttore.Background = Brushes.Gray;
            else
                cvInterruttore.Background = Brushes.Green;
        }
        private void onD4_Click(object sender, RoutedEventArgs e)
        {
            // Digital Write pin=4, valore=1 (on)
            string cmd = String.Format("DW.4.1!");
            SendCommand(cmd);
        }
        private void offD4_Click(object sender, RoutedEventArgs e)
        {
            // Digital Write pin=4, valore=0 (off)
            string cmd = String.Format("DW.4.0!");
            SendCommand(cmd);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Usiamo il field perche la property forza una istanza quindi sarebbe inutile chiaamare la Close.
            if( _com1 != null )
                _com1.Close();
        }

        private string[] SendCommand(string cmd)
        {
            string[] valori = new string[] { "0", "0" };

            try
            {
                com1.Write(cmd);
                Thread.Sleep(200);

                string retVal = com1.ReadLine();
                lblComandoArrivato.Content = retVal;
                valori = retVal.Split('=');
            }
            catch { }

            return valori;
        }

    }
}
