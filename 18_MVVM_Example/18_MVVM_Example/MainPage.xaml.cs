using _18_MVVM_Example.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace _18_MVVM_Example
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {


            this.InitializeComponent();

            this.DataContext = new MainPageViewModel();

            Messenger.Default.Register<ShowMessageDialog>
            (
                this,
                (action) => ReceiveMessage(action)
            );
        }

        private async void ReceiveMessage(ShowMessageDialog action)
        {
            var messageDialog = new MessageDialog(action.Message);

            await messageDialog.ShowAsync();

        }

        private void cboIdiomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Canvi programàtic d'idioma
            //-------------------------------------------------
            ApplicationLanguages.PrimaryLanguageOverride =
                cboIdiomes.SelectedValue.ToString();
            
            this.Frame.Navigate(typeof(MainPage));            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboIdiomes.ItemsSource = 
                new List<string> { "ca-es", "es-es", "en-us" };
        }
    }
}
