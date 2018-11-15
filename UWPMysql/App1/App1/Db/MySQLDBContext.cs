using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App1.Db
{
    class MySQLDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseMySQL(@"User Id=root;Host=localhost;Database=empresa;");
        }




        public static void getHotels()
        {
            

            using (MySQLDBContext ctx = new MySQLDBContext())
            {

                using (var connection = ctx.Database.GetDbConnection())
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "select * from DEPT";
                        DbDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Int64 id = reader.GetInt64(reader.GetOrdinal("DEPT_NO"));
                            string nom = reader.GetString(reader.GetOrdinal("DNOM"));
                            string poblacio = reader.GetString(reader.GetOrdinal("LOC")); 
                        }
                    }
                }
            }
 
        }

        internal static String getQuery(string query)
        {
            String res = "";
            using (MySQLDBContext ctx = new MySQLDBContext())
            {

                using (var connection = ctx.Database.GetDbConnection())
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        DbDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            object[] valors = new object[100];
                            int num = reader.GetValues(valors);
                            for(int i=0;i<num;i++)
                            {
                                res += valors[i].ToString() + "\t\t";
                            }
                            res +=  "\n";
 
                        }
                    }
                }
            }
            return res;
        }
    }
}
