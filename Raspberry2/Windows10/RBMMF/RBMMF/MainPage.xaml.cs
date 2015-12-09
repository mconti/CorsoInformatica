using System;
using PubNubMessaging.Core;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GrovePi;
using GrovePi.Sensors;
using System.Threading.Tasks;
using Windows.Media.Capture;
using System.Collections.Generic;

using Newtonsoft.Json;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace RBMMF
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Pubnub pubnub = new Pubnub(
            "pub-c-65914541-3bbd-4fa9-979d-ffe4b018be8f",
            "sub-c-12fa7c6c-8534-11e5-83e3-02ee2ddab7fe"
        );

        IBuildGroveDevices GrovePi { get; }

        DispatcherTimer timer { get; }

        int ContatoreErrori0 { get; set; }
        int ContatoreErrori1 { get; set; }
        int ContatoreErrori2 { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            GrovePi = DeviceFactory.Build;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();

            ContatoreErrori0 = 0;
            ContatoreErrori1 = 0;
            ContatoreErrori2 = 0;

        }

        private void Timer_Tick(object sender, object e)
        {
            timer.Stop();

            try
            {
                analogVal0.Value = GrovePi.LightSensor(Pin.AnalogPin0).SensorValue();
                lblValue0.Text = analogVal0.Value.ToString();

                //analogVal.Value = GrovePi.TemperatureAndHumiditySensor(Pin.AnalogPin2, Model.OnePointTwo).TemperatureInCelcius();
                Task.Delay(400).Wait();
            }
            catch (Exception)
            {
                ContatoreErrori0++;
                lblErrorCounter0.Text = ContatoreErrori0.ToString();
            }

            try
            {
                analogVal1.Value = GrovePi.LightSensor(Pin.AnalogPin1).SensorValue();
                lblValue1.Text = analogVal1.Value.ToString();

                //analogVal.Value = GrovePi.TemperatureAndHumiditySensor(Pin.AnalogPin2, Model.OnePointTwo).TemperatureInCelcius();
                Task.Delay(400).Wait();
            }
            catch (Exception)
            {
                ContatoreErrori1++;
                lblErrorCounter1.Text = ContatoreErrori1.ToString();
            }

            try
            {
                analogVal2.Value = GrovePi.LightSensor(Pin.AnalogPin2).SensorValue();
                lblValue2.Text = analogVal2.Value.ToString();
                //analogVal.Value = GrovePi.TemperatureAndHumiditySensor(Pin.AnalogPin2, Model.OnePointTwo).TemperatureInCelcius();
                Task.Delay(400).Wait();
            }
            catch (Exception)
            {
                ContatoreErrori2++;
                lblErrorCounter2.Text = ContatoreErrori2.ToString();
            }

            //if (analogVal.Value > 500)
            //{
            //    GrovePi.Relay(Pin.DigitalPin2).ChangeState(SensorStatus.On);
            //    Task.Delay(400);
            //    GrovePi.Relay(Pin.DigitalPin2).ChangeState(SensorStatus.Off);
            //}

            try
            {
                Campione c = new Campione { Temperatura = analogVal0.Value, Luminosita = analogVal1.Value, Rumore = analogVal2.Value };
                string s = "Test";

                // Genera una eccezione!
                s = JsonConvert.SerializeObject(c);

                pubnub.Publish<string>(
                    "Canale1",
                    s,
                    false,
                    DisplayReturnMessage,
                    DisplayErrorMessage
                );
            }
            catch (Exception Err)
            {
            }

            //string s = @"{""eon"": { ""Temperatura"": " + analogVal0.Value + @", ""Luminosita"": " + analogVal2.Value + @",""Rumore"": " + analogVal2.Value + "} }";


            // Rifacciamo un altro giro
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if( MySplitView.IsPaneOpen )
                MySplitView.IsPaneOpen = true;
            else
                MySplitView.IsPaneOpen = false;

            //pubnub.Publish<string>(
            //    "Canale1", "Ciao Dal rasp pi della fiera!", false, 
            //    DisplayReturnMessage, 
            //    DisplayErrorMessage
            //);
        }
        public void MyHistory(object result)
        {
            List<object> history = result as List<object>;
            if (history != null )
            {
                List<object> valori = history[0] as List<object>;
                if( valori != null) { 
                    Debug.WriteLine("History Count:" + valori.Count.ToString());
                    foreach (var val in valori)
                        Debug.WriteLine(val.ToString());
                }
            }
        }
        public void MyHistoryError(object result)
        {
            Debug.WriteLine("HistoryError called...");
        }

        public async void DisplayReturnMessage(string result)
        {
            Debug.WriteLine(".");

            //await MySplitView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {

            //    analogVal.Value = GrovePi.LightSensor(Pin.AnalogPin2).SensorValue();
            //    //analogVal.Value = GrovePi.TemperatureAndHumiditySensor(Pin.AnalogPin2, Model.OnePointTwo).TemperatureInCelcius();

            //    if (analogVal.Value > 500)
            //    {
            //        GrovePi.Relay(Pin.DigitalPin2).ChangeState(SensorStatus.On);
            //        Task.Delay(400);
            //        GrovePi.Relay(Pin.DigitalPin2).ChangeState(SensorStatus.Off);
            //    }

            //    // Rifacciamo un altro giro
            //    timer.Start();

            //});

        }
        void DisplayErrorMessage(PubnubClientError result)
        {
            Debug.WriteLine("");
            Debug.WriteLine(result.Description);
            Debug.WriteLine("");

            switch (result.StatusCode)
            {
                case 103:
                    //Warning: Verify origin host name and internet connectivity
                    break;
                case 104:
                    //Critical: Verify your cipher key
                    break;
                case 106:
                    //Warning: Check network/internet connection
                    break;
                case 108:
                    //Warning: Check network/internet connection
                    break;
                case 109:
                    //Warning: No network/internet connection. Please check network/internet connection
                    break;
                case 110:
                    //Informational: Network/internet connection is back. Active subscriber/presence channels will be restored.
                    break;
                case 111:
                    //Informational: Duplicate channel subscription is not allowed. Internally Pubnub API removes the duplicates before processing.
                    break;
                case 112:
                    //Informational: Channel Already Subscribed/Presence Subscribed. Duplicate channel subscription not allowed
                    break;
                case 113:
                    //Informational: Channel Already Presence-Subscribed. Duplicate channel presence-subscription not allowed
                    break;
                case 114:
                    //Warning: Please verify your cipher key
                    break;
                case 115:
                    //Warning: Protocol Error. Please contact PubNub with error details.
                    break;
                case 116:
                    //Warning: ServerProtocolViolation. Please contact PubNub with error details.
                    break;
                case 117:
                    //Informational: Input contains invalid channel name
                    break;
                case 118:
                    //Informational: Channel not subscribed yet
                    break;
                case 119:
                    //Informational: Channel not subscribed for presence yet
                    break;
                case 120:
                    //Informational: Incomplete unsubscribe. Try again for unsubscribe.
                    break;
                case 121:
                    //Informational: Incomplete presence-unsubscribe. Try again for presence-unsubscribe.
                    break;
                case 122:
                    //Informational: Network/Internet connection not available. C# client retrying again to verify connection. No action is needed from your side.
                    break;
                case 123:
                    //Informational: During non-availability of network/internet, max retries for connection were attempted. So unsubscribed the channel.
                    break;
                case 124:
                    //Informational: During non-availability of network/internet, max retries for connection were attempted. So presence-unsubscribed the channel.
                    break;
                case 125:
                    //Informational: Publish operation timeout occured.
                    break;
                case 126:
                    //Informational: HereNow operation timeout occured
                    break;
                case 127:
                    //Informational: Detailed History operation timeout occured
                    break;
                case 128:
                    //Informational: Time operation timeout occured
                    break;
                case 4000:
                    //Warning: Message too large. Your message was not sent. Try to send this again smaller sized
                    break;
                case 4001:
                    //Warning: Bad Request. Please check the entered inputs or web request URL
                    break;
                case 4002:
                    //Warning: Invalid Key. Please verify the publish key
                    break;
                case 4010:
                    //Critical: Please provide correct subscribe key. This corresponds to a 401 on the server due to a bad sub key
                    break;
                case 4020:
                    // PAM is not enabled. Please contact PubNub support
                    break;
                case 4030:
                    //Warning: Not authorized. Check the permimissions on the channel. Also verify authentication key, to check access.
                    break;
                case 4031:
                    //Warning: Incorrect public key or secret key.
                    break;
                case 4140:
                    //Warning: Length of the URL is too long. Reduce the length by reducing subscription/presence channels or grant/revoke/audit channels/auth key list
                    break;
                case 5000:
                    //Critical: Internal Server Error. Unexpected error occured at PubNub Server. Please try again. If same problem persists, please contact PubNub support
                    break;
                case 5020:
                    //Critical: Bad Gateway. Unexpected error occured at PubNub Server. Please try again. If same problem persists, please contact PubNub support
                    break;
                case 5040:
                    //Critical: Gateway Timeout. No response from server due to PubNub server timeout. Please try again. If same problem persists, please contact PubNub support
                    break;
                case 0:
                    //Undocumented error. Please contact PubNub support with full error object details for further investigation
                    break;
                default:
                    break;
            }

            // 
            //if (showErrorMessageSegments)
            //{
            //    DisplayErrorMessageSegments(result);
            //    Console.WriteLine();
            //}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Arrivano solo quelli degli altri... non i propri!!!
            pubnub.DetailedHistory("Canale1", 10, MyHistory, MyHistoryError);

        }
    }


    public class Campione
    {
        public double Temperatura { get; set; }
        public double Luminosita { get; set; }
        public double Rumore { get; set; }
    }
}
