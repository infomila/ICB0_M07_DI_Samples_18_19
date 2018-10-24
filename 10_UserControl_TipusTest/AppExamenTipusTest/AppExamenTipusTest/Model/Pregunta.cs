using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExamenTipusTest.Model
{
    public class Pregunta
    {
        // SINGLETON <---

        private static List<Pregunta> sPreguntes;

        public static List<Pregunta> getPreguntes()
        {
            if (sPreguntes == null)
            {
                sPreguntes = new List<Pregunta>();
                Pregunta p = new Pregunta("Que dona 2+2?", true, 1);
                p.addOpcio("1+3", true);
                p.addOpcio("2", false);
                p.addOpcio("3", false);
                p.addOpcio("4", true);
                p.addOpcio("Cap de les anteriors.", false);
                sPreguntes.Add(p);
                //-----------------------------------------------------
                p = new Pregunta("Que dona 2-2?", false, 1);
                p.addOpcio("0", true);
                p.addOpcio("-2", false);
                p.addOpcio("2", false);
                p.addOpcio("4", false);
                sPreguntes.Add(p);
                //-----------------------------------------------------
                p = new Pregunta("De que color es el caballo blanco de Santiago?", false, 1);
                p.addOpcio("#ffffff", true);
                p.addOpcio("#000000", false);
                p.addOpcio("No tenia caballo.", false);
                sPreguntes.Add(p);
                //-----------------------------------------------------
            }
            return sPreguntes;
        }

        //--------------------------------------

        private String enunciat;
        private List<String> opcions;
        private Dictionary<int, bool> opcionsCorrectes;
        private Dictionary<int, bool> opcionsMarcades;
        private bool esMultiresposta;
        private decimal puntuacioMaxima;

        public Pregunta(string enunciat, 
            bool esMultiresposta, 
            decimal puntuacioMaxima)
        {
            this.enunciat = enunciat;
            this.opcions = new List<string>();
            this.opcionsCorrectes = new Dictionary<int, bool>();
            this.opcionsMarcades = new Dictionary<int, bool>();
            this.esMultiresposta = esMultiresposta;
            this.puntuacioMaxima = puntuacioMaxima;
        }

        private int numeroRespostesCorrectes = 0;

        public void addOpcio(string novaOpcio, bool esCorrecta)
        {
            opcions.Add(novaOpcio);
            opcionsCorrectes[opcions.Count-1] = esCorrecta;
            opcionsMarcades[opcions.Count - 1] = false;
            //--------------------------------------------------
            if (esCorrecta) numeroRespostesCorrectes++;
        }

        public bool esOpcioCorrecta(int indexOpcio)
        {
            return opcionsCorrectes[indexOpcio];
        }

        public void marcaOpcio(int indexOpcio, bool esMarcada)
        {
            opcionsMarcades[indexOpcio] = esMarcada;
        }

        public string Enunciat { get => enunciat; set => enunciat = value; }
        public List<string> Opcions { get => opcions; set => opcions = value; }
        
        public bool EsMultiresposta { get => esMultiresposta; set => esMultiresposta = value; }
        public decimal PuntuacioMaxima { get => puntuacioMaxima; set => puntuacioMaxima = value; }

        public int GetNumRespostesCorrectes()
        {
            return numeroRespostesCorrectes;
        }

        //------------------------------ DEATH ZONE ----------------------------------------
        //   BIOLOGICAL HAZARD
        //---------------------------------------------------------------------------------

        public Decimal GetPuntuacio()
        {
            Decimal puntuacio;
            int i = 0;
            int encerts = 0;
            int errors = 0;
            if (EsMultiresposta)  // MULTIRESPOSTA ===================================================
            {
                // Usem opcionsMarcades, diccionari <int, bool> 
                //   diccionari[0] --> false
                //   diccionari[1] --> true                
                foreach (int index in opcionsMarcades.Keys)
                {
                    if (opcionsMarcades[index] == true)
                    {
                        if (esOpcioCorrecta(i))
                        {
                            encerts++;
                        }
                        else
                        {
                            encerts--;
                        }
                    }
                    i++;
                }
                // la fòrmula és puntuació pregunta * encerts/número respostes vàlides
                puntuacio = PuntuacioMaxima * (decimal)encerts / (decimal)GetNumRespostesCorrectes();

            }
            else // RADIOBUTTON: mono resposta =================================================
            {
                errors = 0;
                encerts = 0;
                foreach (int index in opcionsMarcades.Keys)
                {
                    if (opcionsMarcades[index] == true)
                    {
                        if (esOpcioCorrecta(i))
                        {
                            encerts = 1;
                            errors = 0;
                        }
                        else
                        {
                            errors = 1;
                            encerts = 0;
                        }
                        break;
                    }
                    i++;
                }
                if (encerts > 0)
                {
                    puntuacio = PuntuacioMaxima;
                }
                else if (errors > 0)
                {
                    puntuacio = -PuntuacioMaxima * (decimal)(1.0 / Opcions.Count);
                }
                else
                {
                    puntuacio = 0;
                }
            }

            return puntuacio;
        }
        //-----------------------------------------------------------------------------------
    }
}
