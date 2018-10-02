using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.model
{
    class Jugador
    {
        private int mDorsal;
        private string mNom;
        private string mCognoms;
        //-----------------------------------------
        public Jugador(int dorsal, string nom, string cognoms)
        {
            Dorsal = dorsal;
            Nom = nom;
            Cognoms = cognoms;
        }
        #region propietats
        //---------------------------------------
        public int Dorsal
        {
            get { return mDorsal; }
            set {
                validaDorsal(value);
                mDorsal = value;
            }
        }

      
        public string Nom
        {
            get { return mNom; }
            set {
                validaNom(value);
                mNom = value;
            }
        }



        public string Cognoms
        {
            get { return mCognoms; }
            set {
                validaNom(value);
                mCognoms = value;
            }
        }
        #endregion propietats

        #region validacions
        private void validaDorsal(int d)
        {
            if (d <= 0) throw new Exception("Dorsal Erroni");
        }
        private void validaNom(string nom)
        {
            if (nom == null || nom.Trim().Length < 3) throw new Exception();
        }
        #endregion validacions
    }
}
