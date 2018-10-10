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
        private TextBlock[,] taulerUI = new TextBlock[3, 3];
        //------------------------------------------------------
        private const char EMPTY = (char)0;
        private const char PLAYER0 = '◯';
        private const char PLAYER1 = '✕';
        //------------------------------------------------------
        private int numeroJugada = 0;
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
            foreach (TextBlock t in grdTauler.Children)
            {
                int fila = Grid.GetRow(t);
                int columna = Grid.GetColumn(t);
                taulerUI[ columna, fila] = t;
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
                // incrementem el comptador de jugada
                numeroJugada++;
                                
                if(heGuanyat)
                {
                    finalPartidaAsync(mCurrentPlayer);
                }
                else if (numeroJugada == 9 && !heGuanyat)
                {
                    //mostrar missatge empat
                    finalPartidaAsync(EMPTY);
                }
                else
                {
                    // canvi de jugador
                    mCurrentPlayer = (mCurrentPlayer == PLAYER0) ? PLAYER1 : PLAYER0;
                }                
                
            }
        }

        private async System.Threading.Tasks.Task finalPartidaAsync(char mCurrentPlayer)
        {
            //throw new NotImplementedException();
            numeroJugada = 0;
            string message = "";
            switch(mCurrentPlayer)
            {
                case PLAYER0: message = "Player 0 WINS !";break;
                case PLAYER1: message = "Player 1 WINS !"; break;
                    default: message = "Tie !"; break;
            }
            ContentDialog dialegFinal = new ContentDialog
            {
                Title ="Final de la partida",
                Content = message,
                CloseButtonText="Ok"
            };
            ContentDialogResult resultat = await dialegFinal.ShowAsync();


        }

        private bool mirarSiHeGuanyat(char mCurrentPlayer)
        {
            int[,,] combinacions = 
            {
                //  columnes
                { { 0,0}, { 0,1 }, { 0,2 } },
                { { 1,0}, { 1,1 }, { 1,2 } },
                { { 2,0}, { 2,1 }, { 2,2 } },
                // files
                { { 0,0}, { 1,0 }, { 2,0 } },
                { { 0,1}, { 1,1 }, { 2,1 } },
                { { 0,2}, { 1,2 }, { 2,2 } },
                //diagonals
                { { 0,0}, { 1,1 }, { 2,2 } },
                { { 0,2}, { 1,1 }, { 2,0 } }
            };

            for(int comb=0; comb < combinacions.GetLength(0); comb++)
            {
                int counter = 0;
                for(int cas=0;cas< combinacions.GetLength(1); cas++)
                {
                    int x = combinacions[comb, cas, 0];
                    int y = combinacions[comb, cas, 1];

                    if (tauler[x, y] == mCurrentPlayer) counter++;
                }
                if (counter == 3)
                {
                    // pintar les caselles guanyadores d'un altre color
                    for (int cas = 0; cas < combinacions.GetLength(1); cas++)
                    {
                        int x = combinacions[comb, cas, 0];
                        int y = combinacions[comb, cas, 1];

                        // dona x i y volem trobar el TextBlock
                        taulerUI[y, x].Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));

                        /*foreach (TextBlock t in grdTauler.Children)
                        {
                            int fila = Grid.GetRow(t);
                            int columna = Grid.GetColumn(t);
                            if (y == columna && x == fila)
                            {
                                t.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0));
                                break;
                            }
                        }*/
                    }

                    return true;
                }
            }
            return false;
        }
    }
}
