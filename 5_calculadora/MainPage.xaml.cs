using System;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _5_calculadora
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            txbDisplay.Text = "0";
            operacioAcabadaDeRealitzar = true;

            /////////-------------------------
      

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(operacioAcabadaDeRealitzar)
            {
                txbDisplay.Text = "";
                operacioAcabadaDeRealitzar = false;
            }
            // D'on vinc? Que ha apretat l'usuari 
            Button elPutoBoto = (Button) sender;
            int numero = Int32.Parse( elPutoBoto.Content.ToString() );
            txbDisplay.Text += numero;
        }
        private bool errorOperacio = false;
        private int acumulador=0;
        private char operacioAnterior = '=';
        private bool operacioAcabadaDeRealitzar = false;
        private void Operacio_Click(object sender, RoutedEventArgs e)
        {
            if (operacioAcabadaDeRealitzar && operacioAnterior!='=') return;

            int numeroDisplay = Int32.Parse(txbDisplay.Text);
            Button botoOperacio = (Button)sender;
            switch (operacioAnterior)
            {
                case '+': acumulador += numeroDisplay; break;
                case '-': acumulador -= numeroDisplay; break;
                case '/': if (numeroDisplay == 0)
                    {
                        errorOperacio = true;
                    } else { 
                    acumulador /= numeroDisplay; } break;
                case '*': acumulador *= numeroDisplay; break;
                case '=': acumulador = numeroDisplay; break;
            }
            if (!errorOperacio)
            {
                operacioAnterior = botoOperacio.Content.ToString()[0];
                txbDisplay.Text = acumulador + "";
                
            } else
            {
                txbDisplay.Text = "Error";
                acumulador = 0;
                operacioAnterior = '=';
                errorOperacio = false;
            }
            operacioAcabadaDeRealitzar = true;
        }
    }
}
