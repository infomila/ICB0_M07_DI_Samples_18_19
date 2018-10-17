using HelloBinding.model;
using System;
using System.Collections.Generic;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace HelloBinding.View
{
    public sealed partial class FitxaPersonaUI : UserControl
    {
        public FitxaPersonaUI()
        {
            this.InitializeComponent();
            btnSave.IsEnabled = false;
        }
        //------------------------------------------------------------
        // crear una Dependency Property
        // usar assistent escribint la paraula "propdp"
        public Persona LaPersona
        {
            get { return (Persona)GetValue(LaPersonaProperty); }
            set { SetValue(LaPersonaProperty, value); }
        }
        //-----------------------------------------------------------
        public event EventHandler OnSave;
        //-----------------------------------------------------------


        // Using a DependencyProperty as the backing store for LaPersona.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaPersonaProperty =
            DependencyProperty.Register("LaPersona", typeof(Persona), typeof(FitxaPersonaUI), new PropertyMetadata(0));

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LaPersona.Id = Int32.Parse(txbId.Text);
            LaPersona.Nom = txbNom.Text;
            LaPersona.UrlFoto = txbUrl.Text;

            OnSave?.Invoke(this, new EventArgs());
            //if(OnSave!=null) OnSave.Invoke(this, new EventArgs());
        }

        private void txb_TextChanged(object sender, TextChangedEventArgs e)
        {
            valida();
        }

        private void valida()
        {
            bool idCorrecte = false;
            string errMsg;
            //-----------------------------------------------------------
            // Valida ID
            int id;
            bool conversioCorrecta = Int32.TryParse(txbId.Text, out id);
            if (conversioCorrecta)
            {                
                idCorrecte = Persona.ValidaId(LaPersona.Id,  id, out errMsg);
            }
            mostraError(idCorrecte, txbId);
            //-----------------------------------------------------------
            // Valida nom
            bool nomCorrecte = Persona.ValidaNom(txbNom.Text, out errMsg);
            mostraError(nomCorrecte, txbNom);
            //-----------------------------------------------------------
            // Valida URL
            bool urlCorrecta = Persona.validaURL(txbUrl.Text, out errMsg);
            mostraError(urlCorrecta, txbUrl);
            //-----------------------------------------------------------
            // actualitzem l'estat del botó
            btnSave.IsEnabled = idCorrecte && nomCorrecte && urlCorrecta;

        }

        private void mostraError(bool correcte, TextBox txb)
        {
            Color c = correcte? Colors.Transparent: Colors.BlueViolet;

            txb.Background = new SolidColorBrush(c);
        }
    }
}
