using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_SQLite.Db
{
    class DeptDB
    {

        /// <summary>
        /// Funció que retorna un llistat de departaments
        /// </summary>
        /// <returns></returns>
        public static string GetDepts()
        {
            String resultat="";
            using(SQLiteDBContext context = new SQLiteDBContext())
            {
                using(var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using( var consulta = connexio.CreateCommand())
                    {
                        
                        // query SQL
                        consulta.CommandText = @"select *  
                                                from dept ";
                        var reader = consulta.ExecuteReader();
                        while(reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int dept_no = reader.GetInt32(reader.GetOrdinal("DEPT_NO"));
                            string dnom = reader.GetString(reader.GetOrdinal("DNOM"));
                            string loc = reader.GetString(reader.GetOrdinal("LOC"));

                            resultat += $"{dept_no} \t\t {dnom} \t\t {loc} \n";
                        }
                    }

                }
            }
            return resultat;

        }


        public static Int64 GetNumDepts()
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"select count(*)  
                                                from dept ";
                        return (Int64) consulta.ExecuteScalar();
                    }
                }
            }

        }



        public static void InsertRandomDept()
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        // query SQL
                        consulta.CommandText = @"insert into dept (dept_no, dnom, loc) values ( 50, 'Finances','Tarragona') ";
                        int filesInserides = consulta.ExecuteNonQuery();
                        if(filesInserides!=1)
                        {
                            throw new Exception("Departament no inserit");
                        }
                    }
                }
            }

        }


    }
}
