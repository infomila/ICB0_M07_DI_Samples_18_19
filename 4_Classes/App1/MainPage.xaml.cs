using App1.model;
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
        List<Equip> equips;
        Equip mEquipSeleccionat;

        public MainPage()
        {
            this.InitializeComponent();

            //---------------------------------------------
            equips = Equip.CrearEquips();
            //-------------------------------------------
            //
            // Recórrer les claus del diccionari per obtenir la llista d'equips
            foreach (Equip equip in  equips)
            {
                cboEquips.Items.Add(equip.Id +"-"+equip.Nom);
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
            mEquipSeleccionat = equips[cboEquips.SelectedIndex];
            
            foreach (Jugador jugador in mEquipSeleccionat.Jugadors)
            {
                lsbJugadors.Items.Add("("+jugador.Dorsal+")"+
                                            jugador.Cognoms+","+
                                            jugador.Nom);
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
                foreach(Jugador j in mEquipSeleccionat.Jugadors)
                {
                    if (j.Nom.ToLower().Equals(nomJugadorFiltrat.ToLower()))
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
            Jugador nouJugador =
                new Jugador(666, 
                            txbNouJugador.Text.Trim(), 
                            txbNouJugador.Text.Trim());
            mEquipSeleccionat.Jugadors.Add(nouJugador);
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
            mEquipSeleccionat.Jugadors.RemoveAt(lsbJugadors.SelectedIndex);            
            RefreshLlistaJugadors();
            
        }
    }
}
