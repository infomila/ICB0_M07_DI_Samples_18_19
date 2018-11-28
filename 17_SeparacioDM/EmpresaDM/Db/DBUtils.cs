using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaDM.Db
{
    public class DBUtils
    {
        public static void CrearParametre( string pNom, object pValue, DbCommand pConsulta)
        {
            DbParameter param = pConsulta.CreateParameter();
            param.ParameterName = "@"+ pNom;
            param.Value = pValue;

            // Cal afegir els paràmetres creats a la llista de paràmetres de la consulta
            pConsulta.Parameters.Add(param);
        }

    }
}
