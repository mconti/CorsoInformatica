﻿using System;
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

// Il modello di elemento per la pagina vuota è documentato all'indirizzo http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace RaspApp1
{
    /// <summary>
    /// Pagina vuota che può essere utilizzata autonomamente oppure esplorata all'interno di un frame.
    /// </summary>
    public sealed partial class MaurizioHomePage : Page
    {
        public MaurizioHomePage()
        {
            this.InitializeComponent();

            button1.Content = "Pulsante1";
            button2.Content = "Pulsante2";
        }

        private void PressionePulsante1(object sender, RoutedEventArgs e)
        {
            button1.Content = "Premuto!!";
        }
    }
}
