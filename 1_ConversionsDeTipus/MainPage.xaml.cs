using System;
using System.Collections.Generic;
using System.Globalization;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ConversionsDeTipus
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        

        private void TexboxColorizer(bool esCorrecte, TextBox t)
        {
            SolidColorBrush brush;
            if (!esCorrecte)
            {
                brush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
            }
            else
            {
                // És correcte
                brush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 255, 0));
            }
            t.Background = brush;
        }

        private void Parse_Click( object sender, RoutedEventArgs events)
        {

            // Mirem si la cadena té punts, no estan permesos !!!!
            int posicioPunt = txbNumero.Text.IndexOf(".");
            bool contePunt = txbNumero.Text.Contains(".");

            // Prendre el número del textbox i convertir-lo a double
            double numero;
            bool conversioOk = Double.TryParse( txbNumero.Text,out numero);

            bool totCorrecte = !contePunt && conversioOk;

            TexboxColorizer(totCorrecte, txbNumero);

            if (totCorrecte){
                // És correcte
                CultureInfo ca = new CultureInfo("ca-ES");
                string catala = numero.ToString("###,###.00", ca);

                CultureInfo us = new CultureInfo("en-US");
                string angles = numero.ToString("###,###.00", us);

                txbAngles.Text = angles;
                txbCatala.Text = catala;
            }

            

        }

        private void Parse2_Click(object sender, RoutedEventArgs e)
        {
            double resultat;
            string numeroText = txbNumero2.Text;
            NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.Number;

            //selecció de l'idioma

            CultureInfo idioma;
            if(rdoAngles.IsChecked.HasValue && rdoAngles.IsChecked.Value) {
                idioma = new CultureInfo("en-US");
            } else
            {
                idioma = new CultureInfo("ca-ES");
            }
            
                       
            bool exit = Double.TryParse(numeroText, style, idioma, out resultat);
            if(exit)
            {
                txbResultat2.Text = resultat.ToString("#,###,###.000");
            }
        }
    }
}
