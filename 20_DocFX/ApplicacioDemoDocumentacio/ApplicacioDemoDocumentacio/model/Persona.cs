using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicacioDemoDocumentacio.model
{

    /// <summary>
    /// 
    ///     Introducció
    ///     --------------
    /// 
    ///    La classe *Persona* representa totes les persones de l'aplicació:
    ///    * alumnes
    ///    * professors
    ///    * PAS
    ///    
    /// </summary>
    public class Persona
    {
        /// <summary>
        ///   Constructor de persona donant totes les dades possibles.
        /// </summary>
        /// <param name="id"> Identificador de l'usuari. Ha de ser estrictament positiu.</param>
        /// <param name="nif"> NIF de l'usuari en format **"99999999X"**. **Obligatori** (no pot ser null)</param>
        /// <param name="nom"> Nom de la persona *(mínim 3 caràcters)* . **Obligatori** ( no pot ser null)</param>
        /// <param name="cognom1"> Primer cognom de la persona *(mínim 3 caràcters)* . **Obligatori** ( no pot ser null)</param>
        /// <param name="cognom2"> Segon cognom de la persona *(mínim 3 caràcters)* . **Opcional** (pot ser null)</param>
        public Persona(int id, string nif, string nom, string cognom1, string cognom2)
        {

        }

        /// <summary>
        ///  Posa/llegeix el valor de NIF de la persona. Es valida segons els criteris oficials de càlcul de la lletra del NIF.
        ///  
        /// Format del NIF:
        /// 
        /// | 9 | 9 | 9 | 9 | 9 | 9 | 9 | 9 | X |
        /// |---|---|---|---|---|---|---|---|---|
        /// 
        /// S'accepten digits a zero al davant.
        ///     Incrustació de codi via Markdown
        ///```cs
        ///     
        ///     Persona p = new Persona(1,"11111111H", "Paco", "Maroto", null);
        ///     p.NIF = "12345678Z"; // exemple d'assignació
        ///    
        ///```
        /// </summary>
        /// <example>
        /// 
        ///     Incrustació de codi mitjançant el tag *code*
        ///     <code>
        ///     <![CDATA[
        ///     Persona p = new Persona(1,"11111111H", "Paco", "Maroto", null);
        ///     p.NIF = "12345678Z"; // exemple d'assignació
        ///     ]]>            
        ///     </code>
        ///     
        ///     <code source="../model/Persona.cs" region="exemplesNIF"></code>
        ///     
        /// </example>
        /// <seealso cref="MainPage"/>
        /// <exception cref="System.ArgumentException">Si el NIF no és correcte ( veure descripció ) llança aquesta excepció.</exception>
        public string NIF { get; set; }

        #region exemplesNIF
        private static void provaNIF()
        {
            Persona p = new Persona(1, "11111111H", "Paco", "Maroto", null);
            p.NIF = "12345678Z"; // exemple d'assignació
        }
        #endregion exemplesNIF


    }
}
