using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace _13_Navegacio
{
    class PaginaBase :  Page
    {
        private MainPage mainPage;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

 
            if (e.Parameter != null &&
                e.Parameter.GetType() == typeof(MainPage))
            {
                mainPage = (MainPage)e.Parameter;
            }

            if (mainPage != null) mainPage.notificaNavegacio(this.GetType());

        }
    }
}
