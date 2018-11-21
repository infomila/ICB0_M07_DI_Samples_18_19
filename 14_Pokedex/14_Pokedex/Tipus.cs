using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace _14_Pokedex
{
    public class Tipus
    {
        int id;
        string nom;
        Color color;

        public Tipus(int id, string nom, Color color)
        {
            Id = id;
            Nom = nom;
            Color = color;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public Color Color { get => color; set => color = value; }


        //----------------------------------------------------------------------
        private static double[,] Modifiers = {
            {1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0.5    ,0  ,1  ,1  ,0.5    ,1} ,
            {1  ,0.5    ,0.5    ,1  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,2  ,0.5    ,1  ,0.5    ,1  ,2  ,1} ,
            {1  ,2  ,0.5    ,1  ,0.5    ,1  ,1  ,1  ,2  ,1  ,1  ,1  ,2  ,1  ,0.5    ,1  ,1  ,1} ,
            {1  ,1  ,2  ,0.5    ,0.5    ,1  ,1  ,1  ,0  ,2  ,1  ,1  ,1  ,1  ,0.5    ,1  ,1  ,1} ,
            {1  ,0.5    ,2  ,1  ,0.5    ,1  ,1  ,0.5    ,2  ,0.5    ,1  ,0.5    ,2  ,1  ,0.5    ,1  ,0.5    ,1} ,
            {1  ,0.5    ,0.5    ,1  ,2  ,0.5    ,1  ,1  ,2  ,2  ,1  ,1  ,1  ,1  ,2  ,1  ,0.5    ,1} ,
            {2  ,1  ,1  ,1  ,1  ,2  ,1  ,0.5    ,1  ,0.5    ,0.5    ,0.5    ,2  ,0  ,1  ,2  ,2  ,0.5}   ,
            {1  ,1  ,1  ,1  ,2  ,1  ,1  ,0.5    ,0.5    ,1  ,1  ,1  ,0.5    ,0.5    ,1  ,1  ,0  ,2} ,
            {1  ,2  ,1  ,2  ,0.5    ,1  ,1  ,2  ,1  ,0  ,1  ,0.5    ,2  ,1  ,1  ,1  ,2  ,1} ,
            {1  ,1  ,1  ,0.5    ,2  ,1  ,2  ,1  ,1  ,1  ,1  ,2  ,0.5    ,1  ,1  ,1  ,0.5    ,1} ,
            {1  ,1  ,1  ,1  ,1  ,1  ,2  ,2  ,1  ,1  ,0.5    ,1  ,1  ,1  ,1  ,0  ,0.5    ,1} ,
            {1  ,0.5    ,1  ,1  ,2  ,1  ,0.5    ,0.5    ,1  ,0.5    ,2  ,1  ,1  ,0.5    ,1  ,2  ,0.5    ,0.5}   ,
            {1  ,2  ,1  ,1  ,1  ,2  ,0.5    ,1  ,0.5    ,2  ,1  ,2  ,1  ,1  ,1  ,1  ,0.5    ,1} ,
            {0  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,2  ,1  ,1  ,2  ,1  ,0.5    ,1  ,1} ,
            {1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,2  ,1  ,0.5    ,0} ,
            {1  ,1  ,1  ,1  ,1  ,1  ,0.5    ,1  ,1  ,1  ,2  ,1  ,1  ,2  ,1  ,0.5    ,1  ,0.5}   ,
            {1  ,0.5    ,0.5    ,0.5    ,1  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,2  ,1  ,1  ,1  ,0.5    ,2} ,
            {1  ,0.5    ,1  ,1  ,1  ,1  ,2  ,0.5    ,1  ,1  ,1  ,1  ,1  ,1  ,2  ,2  ,0.5    ,1}
        };

        private static double[,] ModifiersAlGustDelTonIDelDani = {
            {1, 1,  1,  1,  1,  1,  2,  1,  1,  1,  1,  1,  1,  0,  1,  1,  1,  1},
            {1, 0.5    ,    2,  1,  0.5    ,    0.5    ,    1,  1,  2,  1,  1,  0.5    ,    2,  1,  1,  1,  0.5    ,    0.5    },
            {1, 0.5    ,    0.5    ,    2,  2,  0.5    ,    1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0.5    ,    1},
            {1, 1,  1,  0.5    ,    1,  1,  1,  1,  2,  0.5    ,    1,  1,  1,  1,  1,  1,  0.5    ,    1},
            {1, 2,  0.5    ,    0.5    ,    0.5    ,    2,  1,  2,  0.5    ,    2,  1,  2,  1,  1,  1,  1,  1,  1},
            {1, 2,  1,  1,  1,  0.5    ,    2,  1,  1,  1,  1,  1,  2,  1,  1,  1,  2,  1},
            {1, 1,  1,  1,  1,  1,  1,  1,  1,  2,  2,  0.5    ,    0.5    ,    1,  1,  0.5    ,    1,  2},
            {1, 1,  1,  1,  0.5    ,    1,  0.5    ,    0.5    ,    2,  1,  2,  0.5    ,    1,  1,  1,  1,  1,  0.5    },
            {1, 1,  2,  0,  2,  2,  1,  0.5    ,    1,  1,  1,  1,  0.5    ,    1,  1,  1,  1,  1},
            {1, 1,  1,  2,  0.5    ,    2,  0.5    ,    1,  0,  1,  1,  0.5    ,    2,  1,  1,  1,  1,  1},
            {1, 1,  1,  1,  1,  1,  0.5    ,    1,  1,  1,  0.5    ,    2,  1,  2,  1,  2,  1,  1},
            {1, 2,  1,  1,  0.5    ,    1,  0.5    ,    1,  0.5    ,    2,  1,  1,  2,  1,  1,  1,  1,  1},
            {0.5    ,   0.5    ,    2,  1,  2,  1,  2,  0.5    ,    2,  0.5    ,    1,  1,  1,  1,  1,  1,  2,  1},
            {0, 1,  1,  1,  1,  1,  0,  0.5    ,    1,  1,  1,  0.5    ,    1,  2,  1,  2,  1,  1},
            {1, 0.5    ,    0.5    ,    0.5    ,    0.5    ,    2,  1,  1,  1,  1,  1,  1,  1,  1,  2,  1,  1,  2},
            {1, 1,  1,  1,  1,  1,  2,  1,  1,  1,  0,  2,  1,  0.5    ,    1,  0.5    ,    1,  2},
            {0.5    ,   2,  1,  1,  0.5    ,    0.5    ,    2,  0,  2,  0.5    ,    0.5    ,    0.5    ,    0.5    ,    1,  0.5    ,    1,  0.5    ,    0.5    },
            {1, 1,  1,  1,  1,  1,  0.5   , 2,  1,  1,  1,  0.5   , 1,  1,  0,  0.5   , 2,  1}
      };




        public Dictionary<Tipus, double> GetModificadorsDefensa()
        {
            return null;
        }



        //----------------------------------------------------------------------

       public static Tipus GetTipusPerNom(string nom)
        {
            List<Tipus> tots = GetTipusPokemon();
            var pokemonsAmbNom =  tots.Where<Tipus>(x => x.Nom.Equals(nom));
            Tipus[] po = pokemonsAmbNom.ToArray<Tipus>();
            if (po.Length == 1) return po[0];
            else return null;
        }

        private static List<Tipus> sTipusPokemon;

        public static List<Tipus> GetTipusPokemon()
        {
            if(sTipusPokemon==null)
            {
                sTipusPokemon = new List<Tipus>();
                int id = 0;
                sTipusPokemon.Add(new Tipus(id++, "normal", GetColor("#aa9")));
                sTipusPokemon.Add(new Tipus(id++, "fire", GetColor("#f42")));
                sTipusPokemon.Add(new Tipus(id++, "water", GetColor("#39f")));
                sTipusPokemon.Add(new Tipus(id++, "electric", GetColor("#fc3")));
                sTipusPokemon.Add(new Tipus(id++, "grass", GetColor("#7c5")));
                sTipusPokemon.Add(new Tipus(id++, "ice", GetColor("#6cf")));
                sTipusPokemon.Add(new Tipus(id++, "fighting", GetColor("#b54")));
                sTipusPokemon.Add(new Tipus(id++, "poison", GetColor("#a59")));
                sTipusPokemon.Add(new Tipus(id++, "ground", GetColor("#db5")));
                sTipusPokemon.Add(new Tipus(id++, "flying", GetColor("#89f")));
                sTipusPokemon.Add(new Tipus(id++, "psychic", GetColor("#f59")));
                sTipusPokemon.Add(new Tipus(id++, "bug", GetColor("#ab2")));
                sTipusPokemon.Add(new Tipus(id++, "rock", GetColor("#ba6")));
                sTipusPokemon.Add(new Tipus(id++, "ghost", GetColor("#66b")));
                sTipusPokemon.Add(new Tipus(id++, "dragon", GetColor("#76e")));
                sTipusPokemon.Add(new Tipus(id++, "dark", GetColor("#754")));
                sTipusPokemon.Add(new Tipus(id++, "steel", GetColor("#aab")));
                sTipusPokemon.Add(new Tipus(id++, "fairy", GetColor("#e9e")));
                //sTipusPokemon.Add(new Tipus(id++, "curse", GetColor("#698")));
            }
            return sTipusPokemon;
        }


        public static Color GetColor(string hex)
        {
            byte r;
            byte g;
            byte b;
            hex = hex.Replace("#", string.Empty);
            if (hex.Length == 3) //#698 = 669988
            {
                r = (byte)(Convert.ToUInt32(hex.Substring(0, 1)+ hex.Substring(0, 1), 16));
                g = (byte)(Convert.ToUInt32(hex.Substring(1, 1)+ hex.Substring(1, 1), 16));
                b = (byte)(Convert.ToUInt32(hex.Substring(2, 1)+ hex.Substring(2, 1), 16));
            } else
            { //#797964
                r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
                g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
                b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));                
            }
            return Windows.UI.Color.FromArgb(255, r, g, b);
        }
    }
}
