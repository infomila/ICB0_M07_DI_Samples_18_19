using HelloBinding.model;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace HelloBinding
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

        private void cboPersones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectedIndex.Text   = "" + cboPersones.SelectedIndex;
            txbSelectedItem.Text    = "" + cboPersones.SelectedItem;
            txbSelectedValue.Text   = "" + cboPersones.SelectedValue;

            // Important !!!
            Persona p = (Persona)cboPersones.SelectedItem;
            this.DataContext = p;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //cboPersones.Items.Add("Paco");
            //cboPersones.Items.Add("Maria");
            // Queda abolida la utilització de Items, sota pena de capament dolorós i mort.
            //List<string> persones = new List<string> { "Paco", "Maria", "Pep", "Pau", "Pou" };
            List<Persona> persones = Persona.GetLlistaPersones();
            cboPersones.ItemsSource = persones;
            lsvPersones.ItemsSource = persones;

        }

        private void lsvPersones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Persona p = (Persona)lsvPersones.SelectedItem;
            txbId.Text = ""+p.Id;
            txbNom.Text = p.Nom;
            txbUrl.Text = p.UrlFoto;
            imgFotoGran.Source = new BitmapImage(new Uri(p.UrlFoto));
        }
    }
}
