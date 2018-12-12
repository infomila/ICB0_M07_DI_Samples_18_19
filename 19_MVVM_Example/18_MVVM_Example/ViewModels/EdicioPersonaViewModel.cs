using _18_MVVM_Example.Models;
using _18_MVVM_Example.Views;
using GalaSoft.MvvmLight.Messaging;
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

        public EdicioPersonaViewModel(Persona personaEditada)
        {
            mPersonaActual = personaEditada;// Persona.GetPersones()[0];
            ActualizaDadesPersona();
            this.PropertyChanged += Canvis;
            hiHaCanvisPendents = false;
        }

        private void ActualizaDadesPersona()
        {
            this.Nom = mPersonaActual.Nom;
            this.Edat = mPersonaActual.Edat + "";
            this.Actiu = mPersonaActual.Actiu;
            this.EsHome = mPersonaActual.Sexe;
            this.fotoURL = mPersonaActual.ImageURL;
        }

        private void Canvis(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "PucCancelar" && e.PropertyName != "PucDesar")
            {
                PucCancelar = true;
            }
        }

        string nom;
        string edat;
        bool actiu;
        bool esHome;
        string fotoURL;
        private bool hiHaCanvisPendents;

        public event PropertyChangedEventHandler PropertyChanged;

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

        //------------------------------------------
        public SolidColorBrush BackgroundNom
        {
            get
            {
                bool ok = Persona.ValidaNom(Nom);
                if (ok) return new SolidColorBrush(Colors.Transparent);
                else return new SolidColorBrush(Colors.Tomato);
            }
        }

        public bool PucDesar
        {
            get
            {
                return hiHaCanvisPendents && Persona.ValidaNom(Nom) && Persona.ValidaEdat(Edat);
            }
        }


        public bool PucCancelar
        {
            get
            {
                return hiHaCanvisPendents;
            }
            set
            {
                hiHaCanvisPendents = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PucDesar"));
            }
        }


        public void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ActualizaDadesPersona();
            PucCancelar = false;
        }

        public void ButtonDesar_Click(object sender, RoutedEventArgs e)
        {
            mPersonaActual.Nom = this.Nom;
            mPersonaActual.Edat = Int32.Parse(this.Edat);
            mPersonaActual.Actiu = this.Actiu;
            mPersonaActual.Sexe = this.EsHome;
            PucCancelar = false;
            NotificaResultatDesar(true, "");
        }


        private void NotificaResultatDesar(bool isOk, string errMsg)
        {

            // 1.- Crear un missatge ( paquet de dades )
            MissatgeResultatDesarPersona m = new MissatgeResultatDesarPersona(isOk, errMsg);

            // 2.- Enviar el missatge mitjançant el servei de missatgeria.
            Messenger.Default.Send<MissatgeResultatDesarPersona>(m);
        }
    }
}

