using _15_SQLite.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Emp cap;

        public Dept(int deptNo, string dnom, string loc)
        {
            this.deptNo = deptNo;
            this.dnom = dnom;
            this.loc = loc;
        }

        public int DeptNo { get => deptNo; set => deptNo = value; }


        public static bool ValidaNom(int idActual, string nom)
        {
            int numDepts = DeptDB.GetDeptsAmbNom( idActual,  nom);
            if (numDepts > 0) return false;
            return true;
        }

        public static bool ValidaLoc(string loc)
        {
            return loc != null && loc.Trim().Length > 3;
        }


        public string Nom { get => dnom;
            set {
                if (ValidaNom(DeptNo, value))
                {
                    dnom = value;
                }
                else
                {
                    throw new Exception("Nom Invalid");
                }
                // Hem instal·lat el Fody i això ja no ens fa falta !!
                // How to install Fody.PropertyChanged nuget here:
                //      https://github.com/Fody/PropertyChanged
                // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Nom"));
            }
        }



        public string Loc { get => loc;
            set {
                if (ValidaLoc(value))
                {
                    loc = value;
                }
                else
                {
                    throw new Exception("Nom Invalid");
                }                
                // Hem instal·lat el Fody i això ja no ens fa falta !!
                // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Loc"));
            }
        }
        public bool EsEsborrable
        {
            get
            {
                return NumeroEmpleats == 0;
            }
        }

        public int NumeroEmpleats
        {
            get
            {
               return DeptDB.GetNumeroEmpleats(DeptNo);
            }
        }

        public Emp Cap { get => cap; set
            {
                cap = value;
            }
        }


        public ObservableCollection<Emp> Empleats
        {
            get
            {
                return EmpDB.GetEmpleatsDepartament(DeptNo);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
