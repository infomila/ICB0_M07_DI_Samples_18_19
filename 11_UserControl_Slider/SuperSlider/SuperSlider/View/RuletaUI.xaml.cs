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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace SuperSlider.View
{
    public sealed partial class RuletaUI : UserControl
    {
        const int numParticions = 12;
        const double angleMaxim = 360 - (360.0 / numParticions);

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

        double valorAnterior = 0;

        private void ValorChangedCallback()
        {
            RotateTransform rt = (RotateTransform)cnvDial.RenderTransform;
            //rt.Angle = this.Valor * 360.0 / 100;

            //-----------------------------------------------------
            double angleAnterior = this.valorAnterior * angleMaxim / 100;
            double angleDeDesti = this.Valor * angleMaxim / 100;


            double diferenciaAngular = Math.Abs(angleAnterior - angleDeDesti);
            double velocitatAngular = 360.0 / 500.0; // graus per milisegon

            var storyboard = new Storyboard();
            var doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromMilliseconds(diferenciaAngular / velocitatAngular);
            doubleAnimation.EnableDependentAnimation = true;
            doubleAnimation.To = angleDeDesti;
            Storyboard.SetTargetProperty(doubleAnimation, "Angle");
            Storyboard.SetTarget(doubleAnimation, rt);
            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();


            valorAnterior = this.Valor;
        }

        private void Ellipse_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Point p = e.GetCurrentPoint(cnv).Position;
            double x = p.X - (elpDial.Width / 2) -20;
            double y = p.Y - (elpDial.Height/ 2) -20;
            double angle = Math.Atan2(-y, x);
            
            angle = Math.PI / 2 - angle;
            if (angle < 0) angle = 2 * Math.PI + angle;
            double v = (angle / (angleMaxim * Math.PI /180.0)) * 100;

            if (v > 100) v = 100;
            this.Valor = v;
        }
    }
}
