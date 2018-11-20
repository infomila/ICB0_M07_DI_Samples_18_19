using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_SQLite.Model
{
    class Dept: INotifyPropertyChanged
    {
        private int deptNo;
        private string dnom;
        private string loc;

        public Dept(int deptNo, string dnom, string loc)
        {
            this.deptNo = deptNo;
            this.dnom = dnom;
            this.loc = loc;
        }

        public int DeptNo { get => deptNo; set => deptNo = value; }
        public string Nom { get => dnom; set {
                dnom = value;
                // Hem instal·lat el Fody i això ja no ens fa falta !!
                // How to install Fody.PropertyChanged nuget here:
                //      https://github.com/Fody/PropertyChanged
                // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nom"));
            }
        }
        public string Loc { get => loc; set {
                loc = value;
                // Hem instal·lat el Fody i això ja no ens fa falta !!
                // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Loc"));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
