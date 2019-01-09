using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicacioDemoDocumentacio.model
{
    public class Alumne : Persona
    {

        public Alumne(int id, string nif, string nom, string cognom1, string cognom2) :
            base(id, nif, nom, cognom1, cognom2)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retorna els anys que ha estat matriculat l'alumne. Torna zero si no ha estat mai matriculat.</returns>
        public int GetAnysMatriculats()
        {
            return 1;
        }
    }

    

    /*
    public class Alumne extends Persona
    {
        public Alumne(int id, string nif, string nom, string cognom1, string cognom2)
        {
            super(id, nif, nom.....)
        }
    }*/

}
