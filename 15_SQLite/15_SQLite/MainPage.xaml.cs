using _15_SQLite.Db;
using _15_SQLite.Model;
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

namespace _15_SQLite
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
             lsvDepartaments.ItemsSource = DeptDB.GetDepts();
        }


        private void btnFiltrar_Click( object sender, RoutedEventArgs e)
        {
            ActualitzaLlistaDepts();
        }

        private void ActualitzaLlistaDepts()
        {
            Int32 id = -1;
            bool conversioCorrecta = Int32.TryParse(txbIdFilter.Text, out id);
            if (!conversioCorrecta) id = -1;

            lsvDepartaments.ItemsSource = DeptDB.GetDepts(id, txbNomFilter.Text);
        }

        private void btnNetejaFiltre_Click(object sender, RoutedEventArgs e)
        {
            txbIdFilter.Text = "";
            txbNomFilter.Text = "";
            ActualitzaLlistaDepts();
        }

        private void lsvDepartaments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvDepartaments.SelectedItem != null)
            {
                Dept d = (Dept)lsvDepartaments.SelectedItem;
                txbId.Text = "" + d.DeptNo;
                txbNom.Text = d.Nom;
                txbLoc.Text = d.Loc;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Cal fer validacions !!!
            bool totCorrecte = true;

            Int32 id = -1;
            bool conversioCorrecta = Int32.TryParse(txbId.Text, out id);
            if (!conversioCorrecta) totCorrecte = false;

            //-----------------------------------
            if (totCorrecte)
            {
                DeptDB.ActualitzaDepartament(id, txbNom.Text, txbLoc.Text);

                Dept d = (Dept) lsvDepartaments.SelectedItem;
                d.Nom = txbNom.Text;
                d.Loc = txbLoc.Text;
            }

        }
    }
}
