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
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace SuperSlider.View
{
    public sealed partial class SuperSliderUI : UserControl
    {
        public SuperSliderUI()
        {
            this.InitializeComponent();
        }

        private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            recValor.Width =  e.GetCurrentPoint((Rectangle)sender).Position.X;
        }
    }
}
