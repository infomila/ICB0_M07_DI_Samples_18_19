using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TresEnRatlla
{
    enum EnumFitxa
    {
        RES,
        PEO,
        ALFIL,
        TORRE,
        REI,
        DAMA,
        CAVALL
    }

    private class Demo
    {



        public Demo()
        {
            EnumFitxa fitxa = EnumFitxa.DAMA;

            EnumFitxa[,] tauler = new EnumFitxa[8, 8];
            tauler[2, 4] = EnumFitxa.CAVALL;

            Dictionary<EnumFitxa, char> caracterPerTipus = new Dictionary<EnumFitxa, char>();
            caracterPerTipus[EnumFitxa.PEO] = '♟';
            caracterPerTipus[EnumFitxa.CAVALL] = '♞';


            char caracterAPosar = caracterPerTipus[tauler[2,3]];

        }
    }

}
