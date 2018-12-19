using _18_MVVM_Example.Models;
using _18_MVVM_Example.Views;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public MainPageViewModel()
        {
            Persones = Persona.GetPersones();
        }

        private Persona p;

        public Persona PersonaEditada {
            get
            {
                return p;
            }
            set
            {
                p = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PersonaEditada"));
            }
        }

        public ObservableCollection<Persona> Persones { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
      
}
