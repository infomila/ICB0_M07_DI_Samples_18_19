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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace HelloBinding.View
{
    public sealed partial class FitxaPersonaUI : UserControl
    {
        public FitxaPersonaUI()
        {
            this.InitializeComponent();


            
        }

        // crear una Dependency Property
        // usar assistent escribint la paraula "propdp"




        public Persona LaPersona
        {
            get { return (Persona)GetValue(LaPersonaProperty); }
            set { SetValue(LaPersonaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LaPersona.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaPersonaProperty =
            DependencyProperty.Register("LaPersona", typeof(Persona), typeof(FitxaPersonaUI), new PropertyMetadata(0));

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LaPersona.Id = Int32.Parse(txbId.Text);
            LaPersona.Nom = txbNom.Text;
            LaPersona.UrlFoto = txbUrl.Text;
        }
    }
}
