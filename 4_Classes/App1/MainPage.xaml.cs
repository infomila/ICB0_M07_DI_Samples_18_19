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

        private List<string> jugadorsEquipSeleccionat;



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
            verificaNomNouJugador();
            ActualitzaBotoDelete();
        }

        private void cboEquips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Esborrem tots els jugadors que ja hi havia abans.
            RefreshLlistaJugadors();

        }

        private void RefreshLlistaJugadors()
        {
            lsbJugadors.Items.Clear();
            jugadorsEquipSeleccionat = equips[cboEquips.SelectedItem.ToString()];
            jugadorsEquipSeleccionat.Sort();
            foreach (string jugador in jugadorsEquipSeleccionat)
            {
                lsbJugadors.Items.Add(jugador);
            }
            verificaNomNouJugador();
        }

        private void txbNouJugador_TextChanged(object sender, TextChangedEventArgs e)
        {
            verificaNomNouJugador();
        }

        private void verificaNomNouJugador()
        {
            bool botoNouActiu = false;
            //---------------------------------------------------------------
            string nomJugadorFiltrat = txbNouJugador.Text.Trim();
            if (nomJugadorFiltrat.Length>0)
            {
                //string nomEquipSeleccionat = cboEquips.SelectedValue.ToString();
                //List<string> jugadors = equips[nomEquipSeleccionat];
                bool repetitTrobat = false;
                foreach(string j in jugadorsEquipSeleccionat)
                {
                    if (j.ToLower().Equals(nomJugadorFiltrat.ToLower()))
                    {
                        repetitTrobat = true;
                        break;
                    }
                }
                if(!repetitTrobat)
                {
                    botoNouActiu = true;
                }
                /*if (!jugadors.Contains(nomJugadorFiltrat))
                {
                    botoNouActiu = true;
                }*/
            }
            //------------------------
            btnAdd.IsEnabled = botoNouActiu;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            jugadorsEquipSeleccionat.Add(txbNouJugador.Text.Trim());
            RefreshLlistaJugadors();
            
        }

        private void lsbJugadors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActualitzaBotoDelete();
        }

        private void ActualitzaBotoDelete()
        {
            btnDel.IsEnabled = (lsbJugadors.SelectedIndex >= 0);
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            jugadorsEquipSeleccionat.RemoveAt(lsbJugadors.SelectedIndex);
            RefreshLlistaJugadors();
            
        }
    }
}
