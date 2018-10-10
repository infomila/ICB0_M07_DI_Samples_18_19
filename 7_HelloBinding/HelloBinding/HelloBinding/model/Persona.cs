using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloBinding.model
{
    class Persona
    {
        private int id;
        private string nom;
        private string urlFoto;

        public Persona(int id, string nom, string urlFoto)
        {
            this.Id = id;
            this.Nom = nom;
            this.UrlFoto = urlFoto;
        }

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string UrlFoto { get => urlFoto; set => urlFoto = value; }

        public string NomCompost { 
            get {
                return Id+"-"+Nom;
            }
        }

        //--------------------------------------------------------------

        // mètode per aconseguir una llista de persones
        public static List<Persona> GetLlistaPersones()
        {
            List<Persona> persones = new List<Persona>();
            persones.Add(new Persona(10, "Paco", "http://www.pokexperto.net/pokemongo/pokemon/charmander.png"));
            persones.Add(new Persona(20, "Maria", "http://www.pokexperto.net/pokemongo/pokemon/charmeleon.png"));
            persones.Add(new Persona(30, "Mathew", "http://www.pokexperto.net/pokemongo/pokemon/charizard.png"));
            persones.Add(new Persona(40, "Joana", "http://www.pokexperto.net/pokemongo/pokemon/source/pokemon_icon_006_00_shiny.png"));

            return persones;
        }

    }
}
