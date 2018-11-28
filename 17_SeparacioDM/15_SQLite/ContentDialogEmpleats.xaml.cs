using EmpresaDM.Db;
using EmpresaDM.Model;
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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace _15_SQLite
{
    public sealed partial class ContentDialogEmpleats : ContentDialog
    {
        private Dept mDepartament;
        private Emp mEmpleatSeleccionat;

        public Emp EmpleatSeleccionat { get { return mEmpleatSeleccionat; } }

        public ContentDialogEmpleats( Dept departament )
        {
            mDepartament = departament;
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
           
        }
        private bool carregaInicial= false;
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {

            //dgrEmpleats.SelectionChanged -= dgrEmpleats_SelectionChanged;
            dgrEmpleats.ItemsSource = EmpDB.GetEmpleatsDepartament(mDepartament.DeptNo);          
            dgrEmpleats.SelectedItem = mDepartament.Cap;
            //dgrEmpleats.SelectionChanged += dgrEmpleats_SelectionChanged;
            IsPrimaryButtonEnabled = false;
            carregaInicial = true;
        }

        private void dgrEmpleats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mEmpleatSeleccionat = (Emp) dgrEmpleats.SelectedItem;
            if (carregaInicial) { carregaInicial = false; }
            else
            {
                IsPrimaryButtonEnabled = dgrEmpleats.SelectedItem != null;
            }
        }
    }
}
