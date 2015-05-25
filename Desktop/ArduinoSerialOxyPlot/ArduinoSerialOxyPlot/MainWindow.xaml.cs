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

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

using System.ComponentModel;

namespace ArduinoSerialOxyPlot
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        string[] PortNames { get; set; }
        DispatcherTimer timer;
        public PlotModel Modello { get; private set; }


        public MainWindow()
        {
            InitializeComponent();


            PortNames = SerialPort.GetPortNames();
            cboxPortNames.ItemsSource = new List<string>(PortNames);

            if (PortNames.Count() > 1)
                cboxPortNames.SelectedIndex = 1;

            try
            {
                int a = com1.BytesToRead;

                Modello = new PlotModel();
                Modello.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 1 });
                Modello.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });

                Plotter.Model = Modello;

                timer = new DispatcherTimer();
                timer.Interval = new TimeSpan(300);
                timer.Tick += timer_Tick;
                timer.Start();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (com1.BytesToRead > 0)
                {
                    string val = com1.ReadLine();
                    string[] valori = val.Split(';');

                    double x = Convert.ToDouble(valori[0], CultureInfo.InvariantCulture);
                    double y = Convert.ToDouble(valori[1], CultureInfo.InvariantCulture);

                    var s = (LineSeries)Modello.Series[0];
                    s.Points.Add(new DataPoint(x, y));

                    if (s.Points.Count > 100)
                        s.Points.RemoveAt(0);

                    this.Modello.InvalidatePlot(true);
                }
            }
            catch (Exception errore) { MessageBox.Show(errore.Message); }
        }


        SerialPort _com1 = null;

        SerialPort com1
        {
            get
            {
                try
                {
                    if (_com1 == null)
                        _com1 = new SerialPort(cboxPortNames.SelectedValue.ToString(), 38400, Parity.None, 8, StopBits.One);

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
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string property)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }


        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

    }
}
