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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ExemplesLlistes
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            JugantAmbLlistes();
        }

        private void JugantAmbLlistes()
        {
            List<int> notes = new List<int>();
            int i = notes.Count;
            //---------------------------
            notes.Add(5);
            notes.Add(6);
            //------------------ ???
            notes.Add(3);
            notes.Add(10);
            notes.Add(1);
            notes.Add(3);
            notes.Add(4);
            //-------------------------------------------------------
            i = notes.Count();
            //-------------------------------------------------------
            MostraLlista(notes);

            notes.Insert(2, 6);
            MostraLlista(notes);

            notes.RemoveAt(0);
            MostraLlista(notes);

            //-------------------------------------------------
            List<List<string>> jugadors_x_equip = new List<List<string>>();
            //-------------------------------------------------
            //    ______     |                      |
            //   |___a___|   |                      |
            //   |___b___|   |                      |
            //    ___c___    |                      |
            //------------------------------------------------
            List<string> barsa = new List<string>();
            barsa.Add("Messi");
            barsa.Add("Suárez");
            barsa.Add("Piqué");
            jugadors_x_equip.Add(barsa);

            List<string> girona = new List<string>();
            girona.Add("Estuani");
            girona.Add("Portu");
            girona.Add("Bernardo");
            jugadors_x_equip.Add(girona);

            jugadors_x_equip[1][1] = "Portu enhanced";
            jugadors_x_equip[1].Add("Adai");
            girona.Add("Adai");
        }

        private void MostraLlista( List<int> llista)
        {
            string sortida = "";
            string coma = "";
            foreach( int num in llista)
            {
                sortida += coma + num;
                coma = ",";
            }
            txbResultat.Text += sortida + "\n";
        }

    }
}
