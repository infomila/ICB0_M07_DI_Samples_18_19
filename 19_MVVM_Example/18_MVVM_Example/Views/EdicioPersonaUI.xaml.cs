using _18_MVVM_Example.Models;
using _18_MVVM_Example.ViewModels;
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

namespace _18_MVVM_Example.Views
{
    public sealed partial class EdicioPersonaUI : UserControl
    {
        public EdicioPersonaUI()
        {
            this.InitializeComponent();

            ViewModel = new EdicioPersonaViewModel();
            this.DataContext = ViewModel;
        }

        public Persona PersonaEditada
        {
            get { return (Persona)GetValue(PersonaEditadaProperty); }
            set { SetValue(PersonaEditadaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PersonaEditada.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PersonaEditadaProperty =
            DependencyProperty.Register("PersonaEditada", typeof(Persona), 
                typeof(EdicioPersonaUI), new PropertyMetadata(null, PersonaEditadaChangedStatic));

        private static void PersonaEditadaChangedStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EdicioPersonaUI)d).PersonaEditadaChanged();            
        }

        private void PersonaEditadaChanged()
        {
            //---------------------------------------------------
            // Associem el View Model que li correspon a la pàgina
            // al DataContext
            if (PersonaEditada != null)
            {
                ViewModel.PersonaActual = PersonaEditada;
            }
            //---------------------------------------------------  
        }

        public EdicioPersonaViewModel ViewModel { get; set; }

      

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
