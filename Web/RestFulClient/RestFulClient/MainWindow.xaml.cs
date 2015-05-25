using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace RestFulClient
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string baseAddress = @"http://ws.maurizioconti.com";
        //string baseAddress = @"http://localhost:9307";
        string baseAddress = @"http://automobili.gear.host";
        
        public MainWindow()
        {
            InitializeComponent();

            #region JSON Parse/UnParse

            // Crea json da un oggetto
            //Libro l = new Libro{ID="1234", ISBN="4567", Titolo="Titolone..."};
            //string js = JsonConvert.SerializeObject(l);

            #endregion
        }

        #region Modi di accedere a risorse json senza HttpClient
        private void WebRequest_GET()
        {
            try
            {
                // HttpWebRequest è la più antica classe .NET per l'accesso alla rete
                // E' la meno adatta per utilizzi moderni ed è stata surclassata da WebClient (a sua volta surclassata da HttpClient)
                HttpWebRequest http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "GET";

                WebResponse response = http.GetResponse();

                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string jsonString = sr.ReadToEnd();

                List<Libro> listaDiOggetti = JsonConvert.DeserializeObject<List<Libro>>(jsonString);
                // ... qui il codice continua... è solo un esempio

            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void WebClient_GET()
        {
            // WebClient è costruita sulla vecchia WebRequest ed è stata surclassata da HttpClient
            // http://stackoverflow.com/questions/20530152/need-help-deciding-between-httpclient-and-webclient

            // WebClient supporta anche FTP, ma è meno ottimizzata per HTTP e per le chiamate RESTful moderne
            // Non gestisce la concorrenza, ne l'autenticazione, inoltre non può essere usata in WinRT
            // http://stackoverflow.com/questions/4988286/what-difference-is-there-between-webclient-and-httpwebrequest-classes-in-net

            try
            {
                string jsonString = new WebClient().DownloadString("http://ws.maurizioconti.com/api/libro");
                List<Libro> listaDiOggetti = JsonConvert.DeserializeObject<List<Libro>>(jsonString);
                // ... qui il codice continua... è solo un esempio
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }
        #endregion

        private async void AggiornaUI()
        {
            // Per utilizzare HttpClient è necessario installare il pacchetto NuGet    Microsoft.AspNet.WebApi.Client
            try
            {
                using (var client = new HttpClient())
                {
                    // baseAddress è l'URL della WebAPI
                    client.BaseAddress = new Uri(baseAddress);
                    
                    // Volendo prelevare i valori in formato XML, si lavora sugli header HTTP
                    //  client.DefaultRequestHeaders.Accept.Clear();
                    //  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Preleva il json dalla rete
                    // HTTP GET è il verbo per leggere uno o più record (è la 'R' del CRUD)

                    // Ottiene la stringa json... ma è meglio utilizzare GetAsync che ritorna lo stream leggibile con ReadAsAsync<>
                    //string jsonString = await client.GetStringAsync("/api/auto");

                    HttpResponseMessage res = await client.GetAsync("/api/auto");
                    if (res.IsSuccessStatusCode)
                    {
                        // converte i dati json in una List<Auto>
                        //dgDati.ItemsSource = JsonConvert.DeserializeObject<List<Auto>>(jsonString);
                        dgDati.ItemsSource = await res.Content.ReadAsAsync<List<Auto>>();
                    }
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void getBtn_Click(object sender, RoutedEventArgs e)
        {
            AggiornaUI();
        }

        private async void postBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(edtMarca.Text) && !String.IsNullOrEmpty(edtModello.Text))
            {
                // Crea l'oggetto da inserire usando i dati dell'utente
                Auto automobile = new Auto { Marca = edtMarca.Text, Modello = edtModello.Text };

                using (var client = new HttpClient())
                {
                    // baseAddress è l'URL della WebAPI
                    client.BaseAddress = new Uri(baseAddress);

                    // HTTP POST è il verbo per aggiungere un record (è la 'C' del CRUD)
                    HttpResponseMessage response = await client.PostAsJsonAsync("/api/auto", automobile);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri gizmoUrl = response.Headers.Location; 
                        AggiornaUI();
                    }
                }
            }
            else
                MessageBox.Show("Inserire marca e modello...");
        }

        private async void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Ricava dalla grid l'oggetto da cancellare
            var automobile = dgDati.SelectedItem as Auto;
            if( automobile != null)
            {
                using (var client = new HttpClient())
                {
                    // baseAddress è l'URL della WebAPI
                    client.BaseAddress = new Uri(baseAddress);

                    // HTTP DELETE è il verbo per eliminare un record (è la 'D' del CRUD)
                    HttpResponseMessage response = await client.DeleteAsync("/api/auto/" + automobile.ID);

                    if (response.IsSuccessStatusCode)
                        AggiornaUI();
                }
            }
        }

        private async void updateBtn_Click(object sender, RoutedEventArgs e)
        {

            // Ricava dalla grid l'ID del record da aggiornare
            Auto record = dgDati.SelectedItem as Auto;
            string oldID = record.ID;
            
            if (record != null)
            {
                if (!String.IsNullOrEmpty(edtMarca.Text) && !String.IsNullOrEmpty(edtModello.Text))
                {
                    // Crea l'oggetto da spedire ...
                    Auto automobile = new Auto { ID = oldID, Marca = edtMarca.Text, Modello = edtModello.Text };
                    
                    // Procede...
                    using (var client = new HttpClient())
                    {
                        // baseAddress è l'URL della WebAPI
                        client.BaseAddress = new Uri(baseAddress);
                        
                        // HTTP PUT è il verbo HTTP per modificare un record esistente (è la 'U' del CRUD)
                        HttpResponseMessage response = await client.PutAsJsonAsync("/api/auto/" + automobile.ID, automobile);

                        if (response.IsSuccessStatusCode)
                            AggiornaUI();
                    }
                }
            }
        }

        private void dgDati_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                // Ricavo il record selezionato nella datagrid.
                Auto a = e.AddedCells[0].Item as Auto;
                if (a != null)
                {
                    // Ricavo l'URL dell'immagine, 
                    string url = baseAddress + @"/images/" + a.Marca + ".jpg";

                    // Uso l'url per scaricare l'immagine e bindarla alla UI
                    imgFoto.Source = new BitmapImage(new Uri(url));
                }
            }
            catch { }
        }
    }

    public class Libro
    {
        public string ID { get; set; }
        public string Titolo { get; set; }
        public string ISBN { get; set; }
    }

    public class Auto
    {
        public string ID { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }
    }
}
