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

namespace TresEnRatlla
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // definim el tauler
        private char[,] tauler = new char[3,3];
        //------------------------------------------------------
        private const char EMPTY = (char)0;
        private const char PLAYER0 = '◯';
        private const char PLAYER1 = '✕';
        //------------------------------------------------------

        private char mCurrentPlayer;

        public MainPage()
        {
            this.InitializeComponent();

            // programo els clicks de totes les caselles
            foreach(TextBlock tb in grdTauler.Children)
            {
                tb.Tapped += Casella_Tapped;
            }
            // Inicialitzem el tauler a zeros
            for(int i=0;i<tauler.GetLength(0);i++)
            {
                for (int j = 0; j < tauler.GetLength(1); j++)
                {
                    tauler[i, j] = EMPTY;
                }
            }
            //--------------------------------------------------
            mCurrentPlayer = PLAYER0;
        }

        private void Casella_Tapped(object sender, TappedRoutedEventArgs e)
        {

            // la casella està ocupada?
            TextBlock casella = (TextBlock)sender;
            int fila = Grid.GetRow(casella);
            int columna = Grid.GetColumn(casella);
            if (tauler[fila, columna] == EMPTY)
            {
                casella.Text = "" + mCurrentPlayer;
                tauler[fila, columna] = mCurrentPlayer;

                bool heGuanyat = mirarSiHeGuanyat(mCurrentPlayer);

                //-------------------------------------
                mCurrentPlayer = (mCurrentPlayer == PLAYER0) ? PLAYER1 : PLAYER0;
            }

        }

        private bool mirarSiHeGuanyat(char mCurrentPlayer)
        {
            int[,,] combinacions = 
            {
                { { 0,0}, { 0,1 }, { 0,2 } },
                { { 1,0}, { 1,1 }, { 1,2 } },
                { { 2,0}, { 2,1 }, { 2,2 } }
            };
        }
    }
}
