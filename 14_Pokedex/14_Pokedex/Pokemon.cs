using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Windows.Storage;

namespace _14_Pokedex
{
    public class Pokemon
    {

        string nom;
        StorageFile foto;

        double weight;
        double height;
        double catchRate;        
        int hatchSteps;
        double genderRatio;
        string eggGroups;
        string abilities;
        string EVs_;

        int HP, attack, defense, speed, specialAtack, specialDefense;

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
    }
}