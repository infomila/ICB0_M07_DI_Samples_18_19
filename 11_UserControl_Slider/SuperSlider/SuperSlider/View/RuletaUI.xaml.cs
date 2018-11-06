using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class RuletaUI : UserControl
    {
        public RuletaUI()
        {
            this.InitializeComponent();

            /*
                    < Rectangle Width = "6" Height = "20" Fill = "#aaaaaa" Canvas.Top = "0" Canvas.Left = "117" >
                                 < Rectangle.RenderTransform >
                                     < RotateTransform Angle = "45" CenterX = "3" CenterY = "120" >
                                          </ RotateTransform >
                                      </ Rectangle.RenderTransform >
                                  </ Rectangle >
            */
            int numParticions = 12;
            for (int i = 0; i < numParticions; i++)
            {
                Rectangle r = new Rectangle();
                r.Width = 6;
                r.Height = 20;
                r.Fill = new SolidColorBrush(Color.FromArgb(255, 230, 230, 230));
                Canvas.SetLeft(r, 117);
                Canvas.SetTop(r, 0);
                RotateTransform t = new RotateTransform();
                t.Angle = i * 360.0 / numParticions;
                t.CenterX = 3;
                t.CenterY = 120;
                r.RenderTransform = t;

                cnv.Children.Add(r);
            }



        }


        //---------------------------------------------------------


        public double Valor
        {
            get { return (double)GetValue(ValorProperty); }
            set { SetValue(ValorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Valor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValorProperty =
            DependencyProperty.Register("Valor", typeof(double), typeof(RuletaUI), new PropertyMetadata(0,  ValorChangedCallbackStatic));

        private static void ValorChangedCallbackStatic(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RuletaUI r = (RuletaUI)d;
            r.ValorChangedCallback();
        }

        private void ValorChangedCallback()
        {
            RotateTransform rt = (RotateTransform)cnvDial.RenderTransform;
            rt.Angle = this.Valor * 360.0 / 100;
        }
    }
}
