﻿using _18_MVVM_Example.ViewModels;
using _18_MVVM_Example.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Resources.Core;
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
        public MainPageViewModel ViewModel { get; set; }


        public MainPage()
        {

            this.InitializeComponent();

            //---------------------------------------------------
            // Associem el View Model que li correspon a la pàgina
            // al DataContext
            ViewModel = new MainPageViewModel();
            this.DataContext = ViewModel;
            //---------------------------------------------------


            Messenger.Default.Register<MissatgeResultatDesarPersona>
            (
                this,
                (action) => RebreMissatgeResultatDesarPersona(action)
            );
            Messenger.Default.Register<MissatgePersonaEsborrada>
            (
                this,
                (action) => RebreMissatgePersonaEsborrada(action)
            );
            
        }

        private void RebreMissatgePersonaEsborrada(MissatgePersonaEsborrada action)
        {
            if (lsvPersones.Items.Count > 0)
            {
                lsvPersones.SelectedIndex = 0;
            }
        }

        private async void RebreMissatgeResultatDesarPersona(MissatgeResultatDesarPersona m)
        {
            this.lsvPersones.SelectedItem = m.persona;
            string title = m.isOk? "Canvis desats satisfactòriament":"Error desant les dades";
            string content = m.isOk? "": m.errMsg;
            MessageDialog md = new MessageDialog(content, title);
            await md.ShowAsync();
        }

        private void cboIdiomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Canvi programàtic d'idioma
            //-------------------------------------------------
            ApplicationLanguages.PrimaryLanguageOverride =
                cboIdiomes.SelectedValue.ToString();
            ResourceContext.GetForCurrentView().Reset();
            ResourceContext.GetForViewIndependentUse().Reset();
            this.Frame.Navigate(typeof(MainPage));            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cboIdiomes.ItemsSource = 
                new List<string> { "ca-es", "es-es", "en-us" };
        }





        /*
    private async void ReceiveMessage(ShowMessageDialog action)
    {
        var messageDialog = new MessageDialog(action.Message);

        await messageDialog.ShowAsync();

    }*/
    }
}
