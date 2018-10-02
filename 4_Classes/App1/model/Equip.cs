using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.model
{
    class Equip
    {

        private int mId;
        private string mNom;
        private List<Jugador> mJugadors;



        public Equip(int id, string nom)
        {
            Id = id;
            Nom = nom;
            // incialitzar llista de jugadors
            mJugadors = new List<Jugador>();
        }
        #region propietats

        public List<Jugador> Jugadors
        {
            get { return mJugadors; }
        }

        //-------------------------------------------------
        public int Id
        {
            get { return mId; }
            set {
                if (value < 0) throw new Exception("PUFFFF. Ids negatius no permesos");
                mId = value;
            }
        }
        //----------------------------------------------       
        public string Nom
        {
            get { return mNom; }
            set {
                if (value ==null || value.Trim().Length < 3) throw new Exception("Nom d'equip no vàlid.");
                mNom = value;
            }
        }
        #endregion propietats

        public static void CrearEquips()
        {
            Equip e1 = new Equip(1, "Barça");
            Jugador j1 = new Jugador(10, "Lionel", "Messi");
            Jugador j2 = new Jugador(1, "Ter", "Stegen");
            e1.Jugadors.Add(j1);
            e1.Jugadors.Add(j2);

            List<Equip> equips = new List<Equip>();
            equips.Add(e1);
        }
    }



}
