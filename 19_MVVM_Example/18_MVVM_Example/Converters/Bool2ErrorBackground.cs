using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace _18_MVVM_Example.Converters
{
    class Bool2ErrorBackground:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            bool ok = (bool)value;
            if (ok) return new SolidColorBrush(Colors.Transparent);
            else return new SolidColorBrush(Colors.Tomato);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
