using EmpresaDM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EmpresaDM.Db
{
    public class EmpresaDBContext : DbContext
    {
        public enum DB_PROVIDER_ENUM
        {
            SQLITE, MYSQL
        }

        public static DB_PROVIDER_ENUM DB_PROVIDER = DB_PROVIDER_ENUM.SQLITE;

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (DB_PROVIDER == DB_PROVIDER_ENUM.MYSQL)
            {
                Debug.WriteLine("MYSQL");
                optionBuilder.UseMySQL(@"User Id=root;Host=localhost;Database=empresa;");
            }
            else
            {
                Debug.WriteLine("SQLITE");
                optionBuilder.UseSqlite($"Filename={EmpresaDM.Recursos.DB_PATH}/{EmpresaDM.Recursos.DB_FILENAME}");
            }
        }
    }

}

