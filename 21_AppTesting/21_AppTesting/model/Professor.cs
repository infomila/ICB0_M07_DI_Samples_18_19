using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicacioDemoDocumentacio.model
{
    public class Professor : Persona
    {
        public Professor(int id, string nif, string nom, string cognom1, string cognom2) : base(id, nif, nom, cognom1, cognom2)
        {
        }

        public List<Alumne> alumnesTutoritzats;

    }
}
