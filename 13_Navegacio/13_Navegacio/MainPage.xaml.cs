﻿using System;
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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace _13_Navegacio
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {


            if( args.InvokedItem.Equals("Home") )
            {
                frmPrincipal.Navigate(typeof(PaginaA), this);
            }
            if (args.InvokedItem.Equals("Proveidors"))
            {

                frmPrincipal.Navigate(typeof(HubDemo), this);
            }
            else
            {
                frmPrincipal.Navigate(typeof(PaginaB), this);
            }
        }

        public void notificaNavegacio(Type paginaDesti)
        {
            if(paginaDesti == typeof(PaginaA))
            {
                navMenu.SelectedItem = nviHome;
            } else
            {
                navMenu.SelectedItem = nviClients;
            }
        }



    }
}
