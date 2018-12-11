using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_MVVM_Example.Views
{
    class MissatgeResultatDesarPersona
    {
        public bool isOk;
        public string errMsg;

        public MissatgeResultatDesarPersona(bool isOk, string errMsg)
        {
            this.isOk = isOk;
            this.errMsg = errMsg;
        }
    }
}
