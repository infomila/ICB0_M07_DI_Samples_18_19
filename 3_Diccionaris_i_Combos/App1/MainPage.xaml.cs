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

namespace App1
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //        nom_equip    nom_jugador
        private Dictionary<string, List<string>> equips;
        //   Girona --> { Estuani, Josue, ...... }

        public MainPage()
        {
            this.InitializeComponent();

            equips = new Dictionary<string, List<string>>();
            //---------------------------------------------------
            List<string> jugadorsGirona = new List<string>();
            jugadorsGirona.Add("Estuani");
            jugadorsGirona.Add("Pepet");
            jugadorsGirona.Add("Joanet");
            jugadorsGirona.Add("Cigronet");
            //equips.Add("Girona", jugadorsGirona);
            equips["Girona"] = jugadorsGirona;
            //---------------------------------------------------
            List<string> jugadorBarsa = new List<string>();
            string[] noms = new string[]{"Messi", "Pique", "Ter Stegen" };
            jugadorBarsa.AddRange(noms.ToList<string>());
            equips["Barça"] = jugadorBarsa;
            //-------------------------------------------
            //
            // Recórrer les claus del diccionari per obtenir la llista d'equips
            foreach (string equip in  equips.Keys)
            {
                cboEquips.Items.Add(equip);
            }
            // seleccionem la primera opció del ComboBox
            if (cboEquips.Items.Count > 0) // primer verifiquem que hi hagi algun ítem !!
            {
                cboEquips.SelectedIndex = 0;
            }
            //---------------------------------------------

        }

        private void cboEquips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Esborrem tots els jugadors que ja hi havia abans.
            lsbJugadors.Items.Clear();
            List<string> jugadors = equips[cboEquips.SelectedItem.ToString()];
            foreach (string jugador in jugadors)
            {
                lsbJugadors.Items.Add(jugador);
            }

        }
    }
}
