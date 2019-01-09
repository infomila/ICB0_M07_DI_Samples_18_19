using _18_MVVM_Example.Models;
using _18_MVVM_Example.Views;
using GalaSoft.MvvmLight.Messaging;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace _18_MVVM_Example.ViewModels
{
    public class EdicioPersonaViewModel:INotifyPropertyChanged
    {
        private Persona mPersonaActual;
        private string nom;
        private string edat;
        private bool actiu;
        private bool esHome;
        private string fotoURL;
        //-------------------------------------------------------------------------
        public event PropertyChangedEventHandler PropertyChanged;
        //-------------------------------------------------------------------------

        /// <summary>
        /// Constructor buit, que ens posa una persona "null" a l'espera de que ens n'assignin una.
        /// </summary>
        public EdicioPersonaViewModel(){
            PersonaActual = null;
            ActualizaDadesPersona();
        }
        /// <summary>
        /// Constructor amb una persona a editar.
        /// </summary>
        /// <param name="personaEditada"></param>
        public EdicioPersonaViewModel(Persona personaEditada)
        {
            PersonaActual = personaEditada;
         }

        private void ActualizaDadesPersona()
        {
            if(PersonaActual!=null) {
                this.Nom = PersonaActual.Nom;
                this.Edat = PersonaActual.Edat + "";
                this.Actiu = PersonaActual.Actiu;
                this.EsHome = PersonaActual.Sexe;
                this.fotoURL = PersonaActual.ImageURL;                
            } else {
                this.Nom = "";
                this.Edat = "";
                this.Actiu = false;
                this.EsHome = false ;
                this.fotoURL = "ms-appx:///Assets/Square150x150Logo.scale-200.png";
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Foto"));
        }

        private void Canvis(object sender, PropertyChangedEventArgs e)
        {
            // Verifiquem que el canvi afecta a alguna de les propietats modicables
            string[] editableProps =  new string[] { "Nom", "Edat", "Actiu", "EsHome" };
            if (editableProps.ToList<string>().IndexOf(e.PropertyName)>=0)
            {
                PucCancelar = true;
            }
        }


        //======================================================================================
        #region PROPERTY QUE REGULA L'OBJECTE EN EDICIÓ
        //======================================================================================
        public Persona PersonaActual
        {
            get
            {
                return mPersonaActual;
            }
            set
            {
                this.PropertyChanged -= Canvis;
                mPersonaActual = value;
                ActualizaDadesPersona();
                this.PropertyChanged += Canvis;
                PucCancelar = false;
            }
        }

        /// <summary>
        ///  Backup de la persona anterior ( per si hem de tornar-hi )
        /// </summary>
        private Persona PersonaAnterior { get; set; }
        #endregion

        //======================================================================================
        #region PROPERTIES DE DADES
        //======================================================================================
        public string Nom { get => nom; set { nom = value; } }
        public string Edat { get => edat; set => edat = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
        public bool EsHome { get => esHome; set => esHome = value; }

        public BitmapImage Foto
        {
            get
            {
                return new BitmapImage(new Uri(fotoURL));
            }
        }
        #endregion

        //======================================================================================
        #region PROPERTIES DE VALIDACIÓ ( CONNECTADES VIA CONVERTERS )
        //======================================================================================
        public bool NomCorrecte
        {
            get
            {
                return Persona.ValidaNom(Nom);
            }
        }
        public bool EdatCorrecta
        {
            get
            {
                return Persona.ValidaEdat(Edat);
            }
        }
        #endregion


        //======================================================================================
        #region PROPERTIES RELATIVES ALS BOTONS
        //======================================================================================
        public bool PucCancelar
        {
            get;set;
        }

        [DependsOn("PucCancelar")]
        public bool PucDesar
        {
            get
            {
                return PersonaActual != null && PucCancelar && Persona.ValidaNom(Nom) && Persona.ValidaEdat(Edat);
            }
        }
        [DependsOn("PersonaActual")]
        public bool PucEsborrar
        {
            get { return PersonaActual != null && !PersonaActual.IsNew; }

        }

        [DependsOn("PucCancelar")]
        public bool PucAfegir
        {
            get { return !PucCancelar; }
        }
        #endregion

        [DependsOn("PersonaActual")]
        public bool UIControlActiu
        {
            get { return PersonaActual != null; }
        }

        //======================================================================================
        #region PROGRAMACIÓ DELS BOTONS
        //======================================================================================
        public void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (PersonaActual == null) return;

            if( !PersonaActual.IsNew)
            {
                this.PropertyChanged -= Canvis;
                ActualizaDadesPersona();
                this.PropertyChanged += Canvis;
                PucCancelar = false;
            } else
            {
                PersonaActual = PersonaAnterior;
            }
        }

        public void ButtonEsborrar_Click(object sender, RoutedEventArgs e)
        {
            if (PersonaActual == null) return;

            
            Persona.GetPersones().Remove(PersonaActual);
            PersonaActual = null;
            // 1.- Crear un missatge ( paquet de dades )
            MissatgePersonaEsborrada m = new MissatgePersonaEsborrada();
            // 2.- Enviar el missatge mitjançant el servei de missatgeria.
            Messenger.Default.Send<MissatgePersonaEsborrada>(m);

        }

        public void ButtonDesar_Click(object sender, RoutedEventArgs e)
        {
            if (PersonaActual == null) return;

            PersonaActual.Nom = this.Nom;
            PersonaActual.Edat = Int32.Parse(this.Edat);
            PersonaActual.Actiu = this.Actiu;
            PersonaActual.Sexe = this.EsHome;            

            if (PersonaActual.IsNew)
            {
                Persona.GetPersones().Add(PersonaActual);
            }
            // Ja no hi ha canvis pendents
            PucCancelar = false;

            PersonaActual.Save();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PucEsborrar"));

            // 1.- Crear un missatge ( paquet de dades )
            MissatgeResultatDesarPersona m = new MissatgeResultatDesarPersona(PersonaActual, true, "Persona desada correctament");
            // 2.- Enviar el missatge mitjançant el servei de missatgeria.
            Messenger.Default.Send<MissatgeResultatDesarPersona>(m);
        }

        public void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            PersonaAnterior = PersonaActual;
            PersonaActual = new Persona();
            PucCancelar = true;
        }

        #endregion
    }
}

