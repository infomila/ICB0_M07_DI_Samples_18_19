using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace UserControls.View
{
    public sealed partial class NumericTextBox : UserControl
    {
        public NumericTextBox()
        {
            this.InitializeComponent();
        }
        //-----------------------------------
        #region propietats

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(NumericTextBox), new PropertyMetadata(0));



        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set {
                SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericTextBox), new PropertyMetadata(0));

    
        private bool IsCtrlKeyPressed()
        {
            CoreVirtualKeyStates ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
            return (ctrlState & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        }
        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
   
            if(
                (e.Key>=Windows.System.VirtualKey.NumberPad0 && 
               e.Key <= Windows.System.VirtualKey.NumberPad9) ||
                (e.Key >= Windows.System.VirtualKey.Number0 &&
               e.Key <= Windows.System.VirtualKey.Number9))
            {

            } else
            {
                // interceptar la tecla per a que no arribi al TextBox
                e.Handled = true;
            }
        }

        private void txbNumero_Paste(object sender, TextControlPasteEventArgs e)
        {
            e.Handled = true;
        }




        #endregion
        //-----------------------------------


    }
}
