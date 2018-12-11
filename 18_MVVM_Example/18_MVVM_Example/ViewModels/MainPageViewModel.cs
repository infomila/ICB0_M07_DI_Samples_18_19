using _18_MVVM_Example.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace _18_MVVM_Example.ViewModels
{
    class MainPageViewModel:INotifyPropertyChanged
    {

        public MainPageViewModel()
        {
            Persona p = Persona.GetPersones()[0];
            this.nom = p.Nom;
            this.edat = p.Edat + "";
            this.actiu = p.Actiu;
            this.esHome = p.Sexe;
            this.fotoURL = p.ImageURL;
            this.PropertyChanged += Canvis;
        }

        private void Canvis(object sender, PropertyChangedEventArgs e)
        {
            int i = 0;
        }

        string nom;
        string edat;
        bool actiu;
        bool esHome;
        string fotoURL;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get => nom; set { nom = value; } }
        public string Edat { get => edat; set => edat = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
        public bool EsHome { get => esHome; set => esHome = value; }

        public BitmapImage Foto {
            get {
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
                return Persona.ValidaNom(Nom) && Persona.ValidaEdat(Edat);
            }
        }







        /*
        private RelayCommand _showMessageCommand;

        public RelayCommand ShowMessageCommand => _showMessageCommand ?? (_showMessageCommand = new RelayCommand(ShowMessage));


        private void ShowMessage()
        {
            var msg = new ShowMessageDialog { Message = "Hello World" };
            Messenger.Default.Send<ShowMessageDialog>(msg);
        }*/
    }
}
