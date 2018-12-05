using _18_MVVM_Example.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        string nom;
        string edat;
        bool actiu;
        bool esHome;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Nom { get => nom; set => nom = value; }
        public string Edat { get => edat; set => edat = value; }
        public bool Actiu { get => actiu; set => actiu = value; }
        public bool EsHome { get => esHome; set => esHome = value; }


      








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
