using System;
using System.Collections.Generic;

namespace EFLibrary.Models
{
    public partial class Client
    {
        public Client()
        {
            Comanda = new HashSet<Comanda>();
        }

        public string ClientCod { get; set; }
        public string Nom { get; set; }
        public string Direccio { get; set; }
        public string Ciutat { get; set; }
        public string Estat { get; set; }
        public string CodiPostal { get; set; }
        public string Area { get; set; }
        public string Telefon { get; set; }
        public string ReprCod { get; set; }
        public string LimitCredit { get; set; }
        public string Observacions { get; set; }

        public ICollection<Comanda> Comanda { get; set; }
    }
}
