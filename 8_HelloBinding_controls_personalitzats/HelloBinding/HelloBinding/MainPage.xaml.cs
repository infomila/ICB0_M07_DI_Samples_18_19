using HelloBinding.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Persona> mPersones;

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
            mPersones = Persona.GetLlistaPersones();
            cboPersones.ItemsSource = mPersones;
            lsvPersones.ItemsSource = mPersones;

        }

        private void lsvPersones_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // si no hi ha res seleccionat, no tenim res a fer !!!!
            if (lsvPersones.SelectedItem == null) return;

            Persona p = (Persona)lsvPersones.SelectedItem;
            txbId.Text = ""+p.Id;
            txbNom.Text = p.Nom;
            txbUrl.Text = p.UrlFoto;
            imgFotoGran.Source = new BitmapImage(new Uri(p.UrlFoto));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Persona p = (Persona)lsvPersones.SelectedItem;
            try
            {
                p.Id = Int32.Parse(txbId.Text);
            }
            catch (Exception)
            {
            }
            p.Nom = txbNom.Text;            
            p.UrlFoto = txbUrl.Text;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if(lsvPersones.SelectedItem!=null)
            {
                mPersones.RemoveAt(lsvPersones.SelectedIndex);
                //mPersones.Remove((Persona)lsvPersones.SelectedItem);
            }
        }
    }
}
