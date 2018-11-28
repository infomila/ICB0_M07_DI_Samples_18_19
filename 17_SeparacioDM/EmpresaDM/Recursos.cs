using EmpresaDM.Db;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace EmpresaDM
{
    public class Recursos
    {
        public static string DB_FILENAME { get { return "empresa.db"; } }
        public static string DB_PATH { get { return "db"; } }
        public static bool IsSQLite { get { return 
                    EmpresaDBContext.DB_PROVIDER == EmpresaDBContext.DB_PROVIDER_ENUM.SQLITE; } }

        public static Stream GetArxiuDB()
        {
            var assembly = typeof(Recursos).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"EmpresaDM.Assets.{DB_PATH}.{DB_FILENAME}");
            return stream;
        }
    }
}
