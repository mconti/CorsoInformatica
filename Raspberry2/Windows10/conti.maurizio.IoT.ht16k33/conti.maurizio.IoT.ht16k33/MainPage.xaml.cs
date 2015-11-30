using Glovebox.Graphics.Components;
using Glovebox.Graphics.Drivers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace conti.maurizio.IoT.ht16k33
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public LED8x8Matrix Matrix { get; set; }
        DispatcherTimer Timer { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            // Prepara un timer a 20 millisecondi ma non lo fa partire.
            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            Timer.Interval = TimeSpan.FromMilliseconds(20);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Color ColoreNero = Color.FromArgb(255, 0, 0, 0);

            for (int cnt = 0; cnt < 64; cnt++)
            {
                Button btn = new Button();                              // Istanzio un nuovo bottone
                btn.Name = "btn" + cnt.ToString();                      // Do il nome al bottone (btn0, btn1, btn2 ...)
                btn.Background = new SolidColorBrush(ColoreNero);       // lo sfondo è nero
                btn.Margin = new Thickness(5);                          // Ha una leggera cornice di 5 pixel per distanziarsi dal resto
                btn.HorizontalAlignment = HorizontalAlignment.Stretch;  // Occupa orizzontalmente tutto lo spazio disponibile...
                btn.VerticalAlignment = VerticalAlignment.Stretch;      // ... anche verticalmente.
                btn.Click += R_Click;                                   // Risponde al click chiamando un unico event handler
                btn.Tag = cnt;                                          // Una volta nell'event handler, il proprietà Tag ci da una mano a identificarlo

                
                // Posiziona il bottone nella giusta riga e colonna nella grid
                int riga = cnt / 8;
                int colonna = cnt % 8;
                Grid.SetRow(btn, riga);
                Grid.SetColumn(btn, colonna);

                // Aggiunge il bottone alla griglia
                matriceDiLed.Children.Add(btn);
            }

            // Se siamo su una macchina non dotata di Gpio, usciamo.
            var gpio = GpioController.GetDefault();
            if (gpio == null)
                return;

            //MAX7219 driver = new MAX7219(4, MAX7219.Rotate.D90, MAX7219.ChipSelect.CE0);  // 4 panels, rotate 90 degrees, SPI CE0
            //MAX7219 driver = new MAX7219(1, MAX7219.Rotate.D90, MAX7219.Transform.None, MAX7219.ChipSelect.CE0);
            //Ht16K33 driver = new Ht16K33(new byte[] { 0x70, 0x72 }, Ht16K33.Rotate.None);

            Ht16K33BiColor driver = new Ht16K33BiColor(new byte[] { 0x70 }, Ht16K33.Rotate.D90, LedDriver.Display.On, 70, LedDriver.BlinkRate.Off, "I2C1");
            Matrix = new LED8x8Matrix(driver);     // pass the driver to the LED8x8Matrix Graphics Library

            //Matrix.ScrollStringInFromRight("ALMA ", 40, Glovebox.Graphics.BiColour.Green);
            //Matrix.ScrollStringInFromRight("MAKER ", 40, Glovebox.Graphics.BiColour.Yellow);
            //Matrix.ScrollStringInFromRight("FACTORUM ", 40, Glovebox.Graphics.BiColour.Red);


            for (int x = 0; x < 64; x++)
            {
                Matrix.DrawBitmap(((ulong)1 << x), Glovebox.Graphics.BiColour.Red, 0);
                Matrix.FrameDraw();
                RefreshUI();
                Task.Delay(10).Wait();
            }

            for (int x = 63; x >= 0; x--)
            {
                Matrix.DrawBitmap(((ulong)1 << x), Glovebox.Graphics.BiColour.Green, 0);
                Matrix.FrameDraw();
                RefreshUI();
                Task.Delay(10).Wait();
            }

            Matrix.FrameClear();
            Matrix.FrameDraw();
            RefreshUI();
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            // Event handler per tutti i pulsanti.
            // Nel sender abbiamo il pulsante che ha ricevuto il click... ma è un object quindi va fatto un cast a "Button"
            Button btn = sender as Button;
            if (btn != null)
            {
                // Nella proprietà Tag, ci abbiamo metto un contatore da 0 a 63, lo andiamo a riprendere
                int numeroPulsante = Convert.ToInt32(btn.Tag.ToString());

                // Se esiste Matrix, la gpio è a posto... procediamo.
                if (Matrix != null)
                {
                    // Puntiamo all'ennesimo led della matrice (quella sul campo) e ricaviamo il colore.
                    int colorValue = Matrix.Frame[numeroPulsante].ColourValue;
                    switch (colorValue)
                    {
                        case 0:
                            Matrix.FrameSet(Glovebox.Graphics.BiColour.Red, numeroPulsante, 0);
                            //btn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                            break;

                        case 16711680:
                            Matrix.FrameSet(Glovebox.Graphics.BiColour.Green, numeroPulsante, 0);
                            //btn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
                            break;

                        case 65280:
                            Matrix.FrameSet(Glovebox.Graphics.BiColour.Yellow, numeroPulsante, 0);
                            //btn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 0));
                            break;

                        default:
                            Matrix.FrameSet(Glovebox.Graphics.Colour.Black, numeroPulsante, 0);
                            //btn.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                            break;
                    }

                    Matrix.FrameDraw();
                    RefreshUI();
                }
            }
        }
        
        private void RefreshUI()
        {
            if (Matrix != null)
            {
                int cnt = 0;
                foreach (var p in Matrix.Frame)
                {
                    int colorValue = p.ColourValue;
                    var button = FindName("btn" + cnt.ToString()) as Button;

                    if (button != null)
                    {
                        switch (colorValue)
                        {
                            case 0:
                                button.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 0, 0));
                                break;

                            case 16711680:
                                button.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                                break;

                            case 65280:
                                button.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
                                break;

                            default:
                                button.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 20));
                                break;
                        }
                    }

                    cnt++;
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Matrix.FrameClear();
                Matrix.FrameDraw();
                RefreshUI();
            }
        }


        private void btnFigura(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Button b = sender as Button;
                if (b != null)
                {
                    int figuraIdx = Convert.ToInt32(b.Tag.ToString());
                    switch (figuraIdx)
                    {
                        case 0:
                            Matrix.DrawSymbol(Glovebox.Graphics.Grid.Grid8x8.Symbols.Block);
                            break;
                        case 1:
                            Matrix.DrawSymbol(Glovebox.Graphics.Grid.Grid8x8.Symbols.DownArrow);
                            break;
                        case 2:
                            Matrix.DrawSymbol(Glovebox.Graphics.Grid.Grid8x8.Symbols.HappyFace);
                            break;
                        case 3:
                            Matrix.DrawSymbol(Glovebox.Graphics.Grid.Grid8x8.Symbols.Heart);
                            break;
                        case 4:
                            Matrix.DrawSymbol(Glovebox.Graphics.Grid.Grid8x8.Symbols.HourGlass);
                            break;
                    }
                }

                Matrix.FrameDraw();
                RefreshUI();
            }
        }

        private void btnScrollLeft(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Matrix.FrameShiftLeft();
                Matrix.FrameDraw();
                RefreshUI();
            }
        }
        private void btnScrollRight(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Matrix.FrameShiftRight();
                Matrix.FrameDraw();
                RefreshUI();
            }
        }
        private void btnScrollUp(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Matrix.FrameRollUp();
                Matrix.FrameDraw();
                RefreshUI();
            }
        }
        private void btnScrollDown(object sender, RoutedEventArgs e)
        {
            if (Matrix != null)
            {
                Matrix.FrameRollDown();
                Matrix.FrameDraw();
                RefreshUI();
            }
        }

        private async void btnText(object sender, RoutedEventArgs e)
        {
            Timer.Stop();
            Timer.Start();
            string str = edtText.Text;

            // OK
            await Task.Run(() => Matrix.ScrollStringInFromRight(str, 40, Glovebox.Graphics.BiColour.Green));

            // NOn OK
            //Matrix.ScrollStringInFromRight(str, 40, Glovebox.Graphics.BiColour.Green);
        }

        private void Timer_Tick(object sender, object e)
        {
            Timer.Stop();
            //Dispatcher.Invoke(CoreDispatcherPriority.Normal, RefreshUI, this, resultString);
            RefreshUI();
            Timer.Start();
        }
    }
}

