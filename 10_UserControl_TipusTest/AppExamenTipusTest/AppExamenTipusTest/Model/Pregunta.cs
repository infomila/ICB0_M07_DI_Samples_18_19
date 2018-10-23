using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExamenTipusTest.Model
{
    public class Pregunta
    {


        public static List<Pregunta> getPreguntes()
        {
            List<Pregunta> ps = new List<Pregunta>();
            Pregunta p = new Pregunta("Que dona 2+2?", true, 1);
            p.addOpcio("1+3", true);
            p.addOpcio("2", false);
            p.addOpcio("3", false);
            p.addOpcio("4", true);
            p.addOpcio("Cap de les anteriors.", false);
            ps.Add(p);
            //-----------------------------------------------------

            return ps;
        }

        //--------------------------------------

        private String enunciat;
        private List<String> opcions;
        private Dictionary<int, bool> opcionsCorrectes;
        private bool esMultiresposta;
        private decimal puntuacioMaxima;

        public Pregunta(string enunciat, 
            bool esMultiresposta, 
            decimal puntuacioMaxima)
        {
            this.enunciat = enunciat;
            this.opcions = new List<string>();
            this.opcionsCorrectes = new Dictionary<int, bool>();
            this.esMultiresposta = esMultiresposta;
            this.puntuacioMaxima = puntuacioMaxima;
        }

        private int numeroRespostesCorrectes = 0;

        public void addOpcio(string novaOpcio, bool esCorrecta)
        {
            opcions.Add(novaOpcio);
            opcionsCorrectes[opcions.Count-1] = esCorrecta;
            //--------------------------------------------------
            if (esCorrecta) numeroRespostesCorrectes++;
        }

        public bool esOpcioCorrecta(int indexOpcio)
        {
            return opcionsCorrectes[indexOpcio];
        }

        public string Enunciat { get => enunciat; set => enunciat = value; }
        public List<string> Opcions { get => opcions; set => opcions = value; }
        
        public bool EsMultiresposta { get => esMultiresposta; set => esMultiresposta = value; }
        public decimal PuntuacioMaxima { get => puntuacioMaxima; set => puntuacioMaxima = value; }

        public int GetNumRespostesCorrectes()
        {
            return numeroRespostesCorrectes;
        }
    }
}
