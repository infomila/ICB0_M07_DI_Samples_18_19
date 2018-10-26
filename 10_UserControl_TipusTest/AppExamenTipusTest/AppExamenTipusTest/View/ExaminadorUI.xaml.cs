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
    public sealed partial class ExaminadorUI : UserControl
    {
        public ExaminadorUI()
        {
            this.InitializeComponent();
            HaAcabat = false;
        }

        public event EventHandler OnExamenAcabat;

        //--------------------------------------------------------------------
        public List<Pregunta> Preguntes
        {
            get { return (List<Pregunta>)GetValue(PreguntesProperty); }
            set { SetValue(PreguntesProperty, value); }
        }

        private int indexActual = -1;


        #region Properties
        // Using a DependencyProperty as the backing store for Preguntes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreguntesProperty =
            DependencyProperty.Register("Preguntes", typeof(List<Pregunta>), typeof(ExaminadorUI), new PropertyMetadata(0, PreguntesChangedCallback));


        public bool HaAcabat
        {
            get { return (bool)GetValue(HaAcabatProperty); }
            private set { SetValue(HaAcabatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HaAcabat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HaAcabatProperty =
            DependencyProperty.Register("HaAcabat", typeof(bool), typeof(ExaminadorUI), new PropertyMetadata(0));



        public decimal Puntuacio
        {
            get { return (decimal)GetValue(PuntuacioProperty); }
            private set { SetValue(PuntuacioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Puntuacio.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PuntuacioProperty =
            DependencyProperty.Register("Puntuacio", typeof(decimal), typeof(ExaminadorUI), new PropertyMetadata(0));

        #endregion
        //-------------------------------------------------------------------

        // s'executa en el Settter de Preguntes
        private static void PreguntesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ExaminadorUI o = (ExaminadorUI)d;

            if (o.Preguntes != null && o.Preguntes.Count > 0)
            {
                o.indexActual = 0;
                o.mostraPreguntaActual();

            }
            else
                o.indexActual = -1;
        }
        
        private void mostraPreguntaActual()
        {
            puiPreguntaActual.LaPregunta = Preguntes[indexActual];
            btnAnterior.IsEnabled = indexActual > 0;
            btnPosterior.IsEnabled = indexActual < Preguntes.Count-1;
        }

        private void btnPosterior_Click(object sender, RoutedEventArgs e)
        {
            if (indexActual < Preguntes.Count - 1) indexActual++;
            mostraPreguntaActual();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (indexActual > 0) indexActual--;
            mostraPreguntaActual();
        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            HaAcabat = true;
            btnAnterior.IsEnabled = false;
            btnPosterior.IsEnabled = false;
            puiPreguntaActual.IsEnabled = false;
            btnEnviar.IsEnabled = false;
            decimal total = 0;
            foreach( Pregunta p in Preguntes)
            {
                total += p.GetPuntuacio();
            }
            Puntuacio = total;
            OnExamenAcabat?.Invoke(this, new EventArgs());
        }
    }

}
