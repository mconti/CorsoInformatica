using System;
using PubNubMessaging.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Newtonsoft.Json;
using System.Collections.ObjectModel;


// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace conti.maurizio.RBMMFClient
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Pubnub pubnub;
        ObservableCollection<Campione> Campioni;

        public MainPage()
        {
            this.InitializeComponent();
            pubnub = new Pubnub("pub-c-65914541-3bbd-4fa9-979d-ffe4b018be8f", "sub-c-12fa7c6c-8534-11e5-83e3-02ee2ddab7fe");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Campioni = new ObservableCollection<Campione>();
            Campioni.Add(new Campione { Luminosita = 100, Rumore = 0, Temperatura = 10.3 });
            lvData.ItemsSource = Campioni;

            pubnub.Subscribe<string>(

                // Canale
                "Canale1",

                // Event Handler user
                async (string obj) =>
                    await Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => {
                            List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(obj);
                            //string serializedResultMessage = pubnub.JsonPluggableLibrary.SerializeToJsonString(deserializedMessage[0]);
                            Campione campione = JsonConvert.DeserializeObject<Campione>(deserializedMessage[0].ToString());
                            Campioni.Add(campione);
                        }
                    ),

                // Event Handler connection
                async (string obj) =>
                    await lvLog.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => lvLog.Items.Add("Connesso: " + obj)
                    ),

                // Event Handler error
                async (PubnubClientError obj) =>
                    await lvLog.Dispatcher.RunAsync(
                        CoreDispatcherPriority.Normal,
                        () => lvLog.Items.Add("Erore: " + obj.Message)
                    )
            );
        }
    }

    public class Campione
    {
        public double Temperatura { get; set; }
        public double Luminosita { get; set; }
        public double Rumore { get; set; }
    }
}
