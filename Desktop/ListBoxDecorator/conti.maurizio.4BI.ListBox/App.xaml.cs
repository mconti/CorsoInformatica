using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace conti.maurizio._4BI.ListBox
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<Libro> Libri;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Libri = new List<Libro>();
            Libri.Add(new Libro { Autore = "Maurizio", Titolo = "Advanced C#", Edizione = EdizioneLibro.BestSeller });
            Libri.Add(new Libro { Autore = "Fabio", Titolo = "Advanced Matrix", Edizione = EdizioneLibro.InOfferta });

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Console.Beep(5000, 100);
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            Console.Beep(500, 300);
        }
    }
}
