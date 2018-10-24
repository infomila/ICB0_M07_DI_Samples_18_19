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
        public event EventHandler RespostaChanged;


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
                tb.Tag = i;

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
            //int index = opcionsUI.IndexOf((ToggleButton)sender);
            ToggleButton tb =  (ToggleButton)sender;
            LaPregunta.marcaOpcio((int)tb.Tag, false);
            RespostaChanged?.Invoke(this, new EventArgs());
            //recalculaPuntuacio();            
        }

        private void Tb_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = (ToggleButton)sender;
            LaPregunta.marcaOpcio((int)tb.Tag, true);
            RespostaChanged?.Invoke(this, new EventArgs());
            //recalculaPuntuacio();
        }

    }
}
