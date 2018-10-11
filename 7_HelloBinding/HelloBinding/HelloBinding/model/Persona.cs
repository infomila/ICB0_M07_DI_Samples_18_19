using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloBinding.model
{
    class Persona : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private int id;
        private string nom;
        private string urlFoto;

        public Persona(int id, string nom, string urlFoto)
        {
            this.Id = id;
            this.Nom = nom;
            this.UrlFoto = urlFoto;
        }

        public int Id { get => id; set  { id = value; OnPropertyChanged(); } }
        public string Nom { get => nom; set { nom = value; OnPropertyChanged(); } }
        public string UrlFoto { get => urlFoto; set { urlFoto = value; OnPropertyChanged(); } }

        public string NomCompost { 
            get {
                return Id+"-"+Nom;
            }
        }



        //--------------------------------------------------------------

        // mètode per aconseguir una llista de persones
        public static ObservableCollection<Persona> GetLlistaPersones()
        {
            ObservableCollection<Persona> persones = new ObservableCollection<Persona>();
            persones.Add(new Persona(10, "Paco", "http://www.pokexperto.net/pokemongo/pokemon/charmander.png"));
            persones.Add(new Persona(20, "Maria", "http://www.pokexperto.net/pokemongo/pokemon/charmeleon.png"));
            persones.Add(new Persona(30, "Mathew", "http://www.pokexperto.net/pokemongo/pokemon/charizard.png"));
            persones.Add(new Persona(40, "Joana", "http://www.pokexperto.net/pokemongo/pokemon/source/pokemon_icon_006_00_shiny.png"));

            return persones;
        }

    }
}
