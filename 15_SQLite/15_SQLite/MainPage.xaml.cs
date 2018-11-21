using _15_SQLite.Db;
using _15_SQLite.Model;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _15_SQLite
{


    public enum Mode
    {
        NONE,
        EDIT_DET,
        NEW_DEPT
    }

    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Mode mModeActual ;

        public Mode ModeActual {
            get { return mModeActual; }
            set {
                mModeActual = value;
                switch(mModeActual)
                {
                    case Mode.NEW_DEPT:
                    {
                        btnDelete.IsEnabled = true;
                        btnSave.IsEnabled = true;
                        txbId.Text = "";
                        txbNom.Text = "";
                        txbLoc.Text = "";
                        btnDelete.Content = "";
                    } break;
                    case Mode.EDIT_DET:
                    {
                        btnDelete.IsEnabled = true;
                        btnSave.IsEnabled = true;
                        btnDelete.Content = "";
                    }break;
                    default: {
                        btnDelete.IsEnabled = false;
                        btnSave.IsEnabled = false;
                    }break;
                }
            }
        }


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
                ModeActual = Mode.EDIT_DET;

                Dept d = (Dept)lsvDepartaments.SelectedItem;
                txbId.Text = "" + d.DeptNo;
                txbNom.Text = d.Nom;
                txbLoc.Text = d.Loc;
            } else
            {
                ModeActual = Mode.NONE;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Cal fer validacions !!!
            bool totCorrecte = true;

            Int32 id = -1;
            bool conversioCorrecta = Int32.TryParse(txbId.Text, out id);
            //if (!conversioCorrecta) totCorrecte = false;

            //-----------------------------------
            if (totCorrecte)
            {
                if (ModeActual == Mode.EDIT_DET)
                {
                    DeptDB.ActualitzaDepartament(id, txbNom.Text, txbLoc.Text);

                    Dept d = (Dept)lsvDepartaments.SelectedItem;
                    d.Nom = txbNom.Text;
                    d.Loc = txbLoc.Text;
                } else
                {
                    Dept nou = DeptDB.InsertDept(txbNom.Text, txbLoc.Text);
                    // opció cafre
                    // lsvDepartaments.ItemsSource = DeptDB.GetDepts();

                    ((ObservableCollection<Dept>)lsvDepartaments.ItemsSource).Add(nou);
                    lsvDepartaments.SelectedItem = nou;
                }
            }

        }


        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            ModeActual = Mode.NEW_DEPT;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(ModeActual == Mode.EDIT_DET)
            { 
                Int32 id = -1;
                bool conversioCorrecta = Int32.TryParse(txbId.Text, out id);
                DeptDB.esborrarDepartament(id);

                //Dept deptSeleccionat = (Dept) lsvDepartaments.SelectedItem;
                ObservableCollection<Dept> llista = (ObservableCollection<Dept>)lsvDepartaments.ItemsSource;
                llista.RemoveAt(lsvDepartaments.SelectedIndex);
            }
            // seleccionar el primer element (si n'hi ha algun ! )
            if (lsvDepartaments.Items.Count > 0)
            {
                lsvDepartaments.SelectedIndex = 0;
            }
        }
    }
}
