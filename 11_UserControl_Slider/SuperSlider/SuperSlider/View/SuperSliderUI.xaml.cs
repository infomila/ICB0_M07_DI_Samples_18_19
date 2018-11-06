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
        private double MARGE = 15;
        private const double RELACIO_ASPECTE = 10;

        private bool mPointerPressed = false;

        public SuperSliderUI()
        {
            this.InitializeComponent(); 
        }

 

        #region propietats



        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(SuperSliderUI), new PropertyMetadata(100.0));



        public double Valor
        {
            get { return (double)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(double), typeof(SuperSliderUI), new PropertyMetadata(0, ValorChangedCallbackStatic));

        private static void ValorChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SuperSliderUI s = (SuperSliderUI)d;
            s.ValorChangedCallback();
        }

        private void ValorChangedCallback()
        {
            if (this.Valor < 0) {
                this.Valor = 0;
            }
            else if (this.Valor <= this.MaxValue)
            {
                recValor.Width = this.Valor * (recBack.Width - 2 * MARGE) / this.MaxValue;
                Canvas.SetLeft(recHandler, MARGE + recValor.Width - recHandler.Width / 2);
            } else
            {
                this.Valor = this.MaxValue;
            }
                
        }

        public new double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public new static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(SuperSliderUI), new PropertyMetadata(0, WidthChangedCallbackStatic));
        #endregion

        private static void WidthChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SuperSliderUI s = (SuperSliderUI)d;
            s.WidthChangedCallback();
        }
        
        private void WidthChangedCallback()
        {
            // ======================================
            recBack.Width = this.Width;
            recValor.Width = this.Width - 2 * MARGE;
            recBack.Height = this.Width / RELACIO_ASPECTE;
            recValor.Height = recBack.Height - 2 * MARGE;
            // ======================================
            Canvas.SetLeft(recValor, MARGE);
            Canvas.SetTop(recValor, MARGE);
            // ======================================
            // Col·locar handler
            Canvas.SetTop(recHandler, MARGE * 0.5);
            Canvas.SetLeft(recHandler, MARGE * 0.5);
            recHandler.Height = recValor.Height + MARGE;
            recHandler.Width = recHandler.Height / 3;
            // =====================================

        }

        private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            UpdateBar(e);
            mPointerPressed = true;
        }

        private void UpdateBar(PointerRoutedEventArgs e)
        {
            double x = e.GetCurrentPoint(recValor).Position.X;
            x = x > 0 ? x : 0;
            this.Valor = this.MaxValue *( x / (recBack.Width-MARGE*2));

            //recValor.Width = x;
            
        }


        private void Rectangle_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if(mPointerPressed)
            {
                UpdateBar(e);
            }
        }

        private void Rectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            mPointerPressed = false;
;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.ValorChangedCallback();
        }
    }
}
