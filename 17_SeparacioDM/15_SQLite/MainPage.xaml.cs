
using EmpresaDM.Db;
using EmpresaDM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
            dgrEmps.ItemsSource = (IEnumerable<Dept>)lsvDepartaments.ItemsSource;
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
                txbNumEmp.Text = ""+d.NumeroEmpleats;
                btnDelete.IsEnabled = (d.NumeroEmpleats == 0); // el botó esborrar només està actiu si no hi ha empleats
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

        private void txb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lsvDepartaments.SelectedItem != null) {

                bool isOk = true;

                // a) Validem el nom
                Dept d = (Dept)lsvDepartaments.SelectedItem;
                bool nomValid = Dept.ValidaNom(d.DeptNo, txbNom.Text);
                mostraErrors(txbNom, nomValid);
                if (!nomValid) isOk = false;

                // b) Validem la localitat
                bool locValida = Dept.ValidaLoc(txbLoc.Text);
                mostraErrors(txbLoc, locValida);
                if (!locValida) isOk = false;

                // c) Actualitzem l'estat del botó de desar
                btnSave.IsEnabled = isOk;
            }
        }

        private void mostraErrors(TextBox txb, bool nomValid)
        {
            SolidColorBrush colorBrushRed = new SolidColorBrush(Color.FromArgb(255, 200,0,0));
            SolidColorBrush colorBrushWhite = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            txb.Background = nomValid ? colorBrushWhite : colorBrushRed;
        }

  

        private async void dgrEmps_DoubleTappedAsync(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (dgrEmps.SelectedItem != null)
            {
                ContentDialogEmpleats cde = new ContentDialogEmpleats((Dept)dgrEmps.SelectedItem);
                ContentDialogResult res = await cde.ShowAsync();
                if(res == ContentDialogResult.Primary )
                {
                    Dept d = (Dept) dgrEmps.SelectedItem;
                    d.Cap = cde.EmpleatSeleccionat;
                }
            }
        }
    }
}
