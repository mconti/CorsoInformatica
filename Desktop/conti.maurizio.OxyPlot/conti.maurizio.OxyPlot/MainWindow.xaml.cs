
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

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows.Threading;
using System.ComponentModel;

namespace conti.maurizio.OxyPlot
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //
        // Per mettere insieme questo esempio funzionante ho:
        //
        // 1) Nugettato OxyPlot
        // 2) Aggiunto il codice C#
        // 3) Risolto gli using
        // 4) Aggiunto il namespace xmlns:oxy="http://oxyplot.org/wpf" nel codice xaml
        // 5) Aggiunto <oxy:PlotView Grid.Row="1" Model="{Binding Model0}"/> nel codice xaml
        //
        public PlotModel Modello { get; private set; }

        DispatcherTimer timer;
        static double angolo = 0;

        public MainWindow()
        {
            InitializeComponent();

            Modello = new PlotModel();
            Modello.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 1 });
            Modello.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });

            this.DataContext = this;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        
        void timer_Tick(object sender, EventArgs e)
        {
            angolo += 0.1;

            var s = (LineSeries)Modello.Series[0];
            s.Points.Add(new DataPoint(angolo, Math.Sin(angolo)));

            if( s.Points.Count > 100)
                s.Points.RemoveAt(0);

            this.Modello.InvalidatePlot(true);
            
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

    }
}
