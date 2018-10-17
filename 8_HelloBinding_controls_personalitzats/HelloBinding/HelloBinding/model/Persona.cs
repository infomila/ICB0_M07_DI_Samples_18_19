using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloBinding.model
{
    public class Persona : INotifyPropertyChanged
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

        public Persona()
        {
            this.Id = GetLlistaPersones().Select(p => p.Id).Max() + 10;
            this.nom = "";
            this.urlFoto = "http://";
        }

        public Persona(int id, string nom, string urlFoto)
        {
            this.Id = id;
            this.Nom = nom;
            this.UrlFoto = urlFoto;
        }

        public static bool ValidaId( int idActual, int id , out string errMsg)
        {
            errMsg = "";
            if (id == idActual) return true;

            if (id <= 0) { errMsg = "Ids negatius no vàlids." ; return false; }
            /*foreach (Persona p in GetLlistaPersones())
            {
                if (p.Id == id)
                {
                    errMsg = "Id ja existent";
                    return false;
                }
            }*/
            // Lambda expression
            int idsTrobats = GetLlistaPersones().Where(p => p.Id == id).ToArray().Length;
            if (idsTrobats>0)
            {
                errMsg = "Id ja existent.";
                return false;
            }
            return true;
        }

        public static bool ValidaNom(string nom, out string errMsg)
        {
            errMsg = "";
            if (nom == null || nom.Trim().Length < 3)
            {
                errMsg = "Nom no vàlid.";
                return false;
            }
            return true;
        }

        public static bool validaURL(string url, out string errMsg)
        {
            errMsg = "";
            string regexp = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
            if (Regex.Match(url, regexp).Success == false)
            {
                errMsg = "URL incorrecta";
                return false;
            }
            return true;
        }

        public int Id { get => id;
            set  {
                string errMsg;
                if (ValidaId( Id, value, out errMsg)==false) throw new Exception(errMsg);
                id = value; OnPropertyChanged(); }

        }

        public string Nom { get => nom; set {
                string errMsg;
                if (ValidaNom(value, out errMsg) == false) throw new Exception(errMsg);
                nom = value; OnPropertyChanged(); } }

        public string UrlFoto { get => urlFoto;
            set {
                string errMsg;
                if (validaURL(value, out errMsg) == false) throw new Exception(errMsg);
                urlFoto = value;
                OnPropertyChanged(); } }

        public string NomCompost { 
            get {
                return Id+"-"+Nom;
            }
        }



        //--------------------------------------------------------------
        // mètode per aconseguir una llista de persones

        private static ObservableCollection<Persona> sPersones;

        public static ObservableCollection<Persona> GetLlistaPersones()
        {
            if (sPersones == null)
            {

                sPersones = new ObservableCollection<Persona>();
                sPersones.Add(new Persona(10, "Paco", "http://www.pokexperto.net/pokemongo/pokemon/charmander.png"));
                sPersones.Add(new Persona(20, "Maria", "http://www.pokexperto.net/pokemongo/pokemon/charmeleon.png"));
                sPersones.Add(new Persona(30, "Mathew", "http://www.pokexperto.net/pokemongo/pokemon/charizard.png"));
                sPersones.Add(new Persona(40, "Joana", "http://www.pokexperto.net/pokemongo/pokemon/source/pokemon_icon_006_00_shiny.png"));
            }
            return sPersones;
        }

    }
}
