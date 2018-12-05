using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace _18_MVVM_Example.Converters
{
    class Sexe2CheckboxConverter: IValueConverter
    {
        /**
         *  Quan passem dades del ViewMode al View
         *  
         *  value és del tipus de la propietat del ViewModel (bool EsHome)
         *  el retorn és el valor resultant del Binding ( IsChecked del Checkbox )
         *  
         * */
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string HoD = (string)parameter;

            bool esHome = (bool)value;

            return (HoD.Equals("H") && esHome) || (HoD.Equals("D") && !esHome);
        }


        /**
         *  Canvi en la direcció View ---> ViewModel
         *  el value és la propietat que fa Binding ( IsChecked )
         *  el retorn és el valor de la propietat del ViewModel ( bool EsHome )
         */
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            bool isChecked = (bool)value;
            string HoD = (string)parameter;
            if (isChecked) {
                return HoD.Equals("H");
            } else
            {
                // CAS en el que el checkbox es desactiva.
                // Això indica que no es faci cap canvi de valor !!!!!
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
