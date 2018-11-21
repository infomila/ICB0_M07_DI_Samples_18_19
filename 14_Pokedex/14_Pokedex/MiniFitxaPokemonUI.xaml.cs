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

namespace _14_Pokedex
{
    public sealed partial class MiniFitxaPokemonUI : UserControl
    {
        public MiniFitxaPokemonUI()
        {
            this.InitializeComponent();
        }



        public Pokemon MyPokemon
        {
            get { return (Pokemon)GetValue(MyPokemonProperty); }
            set { SetValue(MyPokemonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyPokemon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPokemonProperty =
            DependencyProperty.Register("MyPokemon", typeof(Pokemon), typeof(MiniFitxaPokemonUI), new PropertyMetadata(null));


    }
}
