using AppExamenTipusTest.Model;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace AppExamenTipusTest.View
{
    public sealed partial class PreguntaUI : UserControl
    {
        public PreguntaUI()
        {
            this.InitializeComponent();
        }
        //-----------------------------------------------------------------------------------
        #region propietats de dependència per la UI

        public Pregunta LaPregunta
        {
            get { return (Pregunta)GetValue(LaPreguntaProperty); }
            set { SetValue(LaPreguntaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LaPregunta.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LaPreguntaProperty =
            DependencyProperty.Register("LaPregunta", typeof(Pregunta), typeof(PreguntaUI), new PropertyMetadata(0, PreguntaChangedCallback));

 
        public Decimal Puntuacio
        {
            get { return (Decimal)GetValue(PuntuacioProperty); }
            set { SetValue(PuntuacioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Puntuacio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuntuacioProperty =
            DependencyProperty.Register("Puntuacio", typeof(Decimal), typeof(PreguntaUI), new PropertyMetadata(0));

        #endregion
        //-----------------------------------------------------------------------------------


        private List<ToggleButton> opcionsUI;

        private static void PreguntaChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PreguntaUI pui = (PreguntaUI)d;

            pui.tbkPregunta.Text = pui.LaPregunta.Enunciat;
            /*
             * RadioButton Padding="20,0,0,0" VerticalContentAlignment="Center" 
                     Margin="20,0,0,0" IsChecked="True"
                     Content="a) Festival de la espuma programàtica"></>
             */
            int i = 0;

            pui.opcionsUI = new List<ToggleButton>();

            foreach (string opcio in pui.LaPregunta.Opcions)
            {

                ToggleButton tb;

                if (pui.LaPregunta.EsMultiresposta)
                {
                    tb = new CheckBox();
                }
                else
                {
                   tb = new RadioButton();
                }
                // desem la referència del RadioButton o del Checkbox en una llista
                pui.opcionsUI.Add(tb);

                //-------------------------------------
                // programem l'event per detectar els canvis
                tb.Checked += pui.Tb_Checked;
                tb.Unchecked += pui.Tb_Unchecked;
                //-------------------------------------
                tb.Padding = new Thickness(20, 0, 0, 0);
                tb.Margin = new Thickness(20, 0, 0, 0);
                tb.VerticalContentAlignment = VerticalAlignment.Center;
                tb.IsChecked = false;
                tb.Content = (char)('a' + i) + ") " + opcio;
                pui.stpOpcions.Children.Add(tb);
                i++;
            }
            
        }

  
        private void Tb_Unchecked(object sender, RoutedEventArgs e)
        {
            recalculaPuntuacio();            
        }

        private void Tb_Checked(object sender, RoutedEventArgs e)
        {
            recalculaPuntuacio();
        }

        private void recalculaPuntuacio()
        {
            int i = 0;
            int encerts = 0;
            int errors = 0;
            if (LaPregunta.EsMultiresposta)  // MULTIRESPOSTA ===================================================
            {
                foreach (ToggleButton tb in opcionsUI)
                {
                    if (tb.IsChecked == true)
                    {
                        if (LaPregunta.esOpcioCorrecta(i))
                        {
                            encerts++;
                        }
                        else
                        {
                            encerts--;
                        }
                    }
                    i++;
                }
                // la fòrmula és puntuació pregunta * encerts/número respostes vàlides
                Puntuacio = LaPregunta.PuntuacioMaxima * (decimal)encerts / (decimal)LaPregunta.GetNumRespostesCorrectes();

            }
            else // RADIOBUTTON: mono resposta =================================================
            {
                errors = 0;
                encerts = 0;
                foreach (ToggleButton tb in opcionsUI)
                {
                    if (tb.IsChecked ==  true )
                    {
                        if (LaPregunta.esOpcioCorrecta(i))
                        {
                            encerts = 1;
                            errors = 0;
                        } else
                        {
                            errors = 1;
                            encerts = 0;
                        }
                        break;
                    }
                    i++;
                }
                if(encerts > 0)
                {
                    Puntuacio = LaPregunta.PuntuacioMaxima;
                } else if(errors>0)
                {
                    Puntuacio = -LaPregunta.PuntuacioMaxima * (decimal)( 1.0 / LaPregunta.Opcions.Count );
                } else
                {
                    Puntuacio = 0;
                }
            }

        }
    }
}
