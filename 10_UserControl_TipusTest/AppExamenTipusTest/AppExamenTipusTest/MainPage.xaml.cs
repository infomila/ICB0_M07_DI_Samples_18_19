using AppExamenTipusTest.Model;
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

namespace AppExamenTipusTest
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Carreguem la llista de preguntes en el ListView
            lsvPreguntes.ItemsSource = Pregunta.getPreguntes();
        }

        private void puiPrimeraPregunta_RespostaChanged(object sender, EventArgs e)
        {
        }

        private void btnPuntuacio_Click(object sender, RoutedEventArgs e)
        {
            decimal total = 0;
            foreach (Pregunta p in Pregunta.getPreguntes())             
            {
                total += p.GetPuntuacio();
            }
            txbPuntuacioExamen.Text = total + " punts";
        }
    }
}
