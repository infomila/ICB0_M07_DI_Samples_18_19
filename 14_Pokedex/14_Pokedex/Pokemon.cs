using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace _14_Pokedex
{
    public class Pokemon
    {


        //-------------------------------------------------------------

        private static ObservableCollection<Pokemon> sLlistaPokemons;

        public static ObservableCollection<Pokemon> GetLlistaPokemons()
        {
            if(sLlistaPokemons==null)
            {
                sLlistaPokemons = new ObservableCollection<Pokemon>();

                Pokemon p = new Pokemon(1, 2, 2, 2, 2, 2, "xxx", "cc", "", 2, 2, 2, 2, 2, 2, "kk", Tipus.GetTipusPerNom("fire"));
                Pokemon p2 = new Pokemon(2, 2, 2, 2, 2, 2, "xxx", "cc", "", 2, 2, 2, 2, 2, 2, "kk", Tipus.GetTipusPerNom("fire"), Tipus.GetTipusPerNom("water"));
                p.AfegirEvolucio(p);
                p.AfegirEvolucio(p2);
                p.AfegirFotoDesdeAssets("StoreLogo.png"); // assumirem que aquesta imatge està a assets

                p2.AfegirEvolucio(p);
                p2.AfegirEvolucio(p2);


                sLlistaPokemons.Add(p);
                sLlistaPokemons.Add(p2);
            }
            return sLlistaPokemons;
        }

        private void AfegirFotoDesdeAssets(string pathDinsAssets)
        {
            // donat un arxiu a Assets, el copiem a AplicationData, i en desem la referència.

            this.arxiuFoto = StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/"+ pathDinsAssets)).AsTask().Result;

            Foto = new BitmapImage();
            using (var fileStream = this.arxiuFoto.OpenAsync(FileAccessMode.Read).AsTask().Result)
            {
                Foto.SetSource(fileStream);
            }
        }


        //-------------------------------------------------------------


        int numero;
        string nom;
        StorageFile arxiuFoto;
        BitmapImage foto;

        double weight;
        double height;
        double catchRate;        
        int hatchSteps;
        double genderRatio;
        string eggGroups;
        string abilities;
        string EVs_;

        int HP, attack, defense, speed, specialAtack, specialDefense;

        public Pokemon(int numero, double weight, double height, double catchRate, int hatchSteps, double genderRatio, string eggGroups,
            string abilities, string eVs, int hP1, int attack, int defense, int speed, int specialAtack, int specialDefense, string nom,
            Tipus tipusPrimari, Tipus tipusSecundari = null)
        {
            Numero = numero;
            Weight = weight;
            Height = height;
            CatchRate = catchRate;
            HatchSteps = hatchSteps;
            GenderRatio = genderRatio;
            EggGroups = eggGroups;
            Abilities = abilities;
            EVs = eVs;
            HP1 = hP1;
            Attack = attack;
            Defense = defense;
            Speed = speed;
            SpecialAtack = specialAtack;
            SpecialDefense = specialDefense;
            Nom = nom;

            this.evolucions = new ObservableCollection<Pokemon>();
            this.tipus = new List<Tipus>();
            tipus.Add(tipusPrimari);
            if(tipusSecundari!=null)
            {
                tipus.Add(tipusSecundari);
            }

        }

        ObservableCollection<Pokemon> evolucions;

        List<Tipus> tipus;
    

        //--------------------------------------------------------------------


        public double Weight { get => weight; set => weight = value; }
        public double Height { get => height; set => height = value; }
        public double CatchRate { get => catchRate; set => catchRate = value; }
        public int HatchSteps { get => hatchSteps; set => hatchSteps = value; }
        public double GenderRatio { get => genderRatio; set => genderRatio = value; }
        public string EggGroups { get => eggGroups; set => eggGroups = value; }
        public string Abilities { get => abilities; set => abilities = value; }
        public string EVs { get => EVs_; set => EVs_ = value; }
        public int HP1 { get => HP; set => HP = value; }
        public int Attack { get => attack; set => attack = value; }
        public int Defense { get => defense; set => defense = value; }
        public int Speed { get => speed; set => speed = value; }
        public int SpecialAtack { get => specialAtack; set => specialAtack = value; }
        public int SpecialDefense { get => specialDefense; set => specialDefense = value; }
        public string Nom { get => nom; set => nom = value; }
        public int Numero { get => numero; set => numero = value; }
        public BitmapImage Foto { get => foto; set => foto = value; }



        //-----------------------------------------------------------
        // Gestió d'evolucions

        public void AfegirEvolucio(Pokemon unPokemon)
        {
            this.evolucions.Add(unPokemon);
        }
    
        public void EliminarEvolucio(Pokemon unPokemonAEliminar)
        {
            this.evolucions.Remove(unPokemonAEliminar);
        }

        public ObservableCollection<Pokemon> GetEvolucions()
        {
            return this.evolucions;
        }




    }



}