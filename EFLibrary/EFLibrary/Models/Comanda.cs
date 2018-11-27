using System;
using System.Collections.Generic;

namespace EFLibrary.Models
{
    public partial class Comanda
    {
        public string ComNum { get; set; }
        public string ComData { get; set; }
        public string ComTipus { get; set; }
        public string ClientCod { get; set; }
        public string DataTramesa { get; set; }
        public string Total { get; set; }

        public Client ClientCodNavigation { get; set; }
    }
}
