using _15_SQLite.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15_SQLite.Db
{
    class EmpDB
    {
        public static ObservableCollection<Emp> GetEmpleatsDepartament()
        {
            ObservableCollection<Emp> llista = new ObservableCollection<Emp>();

            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        //------------------------------------------
                        //---    Creació de paràmetres
                        //------------------------------------------

                     
                        //------------------------------------------

                        consulta.CommandText = $@"select *  
                                                from emp  ";

                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int emp_no = reader.GetInt32(reader.GetOrdinal("EMP_NO"));
                            string cognom = reader.GetString(reader.GetOrdinal("COGNOM"));

                            Emp e = new Emp(emp_no, cognom);

                            llista.Add(e);
                        }
                    }
                }
            }
            return llista;

        }

        public static ObservableCollection<Emp> GetEmpleatsDepartament(
            int DeptNo)
        {
            ObservableCollection<Emp> llista = new ObservableCollection<Emp>();

            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {
                        //------------------------------------------
                        //---    Creació de paràmetres
                        //------------------------------------------

                        DBUtils.CrearParametre("DeptNo", DeptNo, consulta);
                        

                        //------------------------------------------

                        consulta.CommandText = $@"select *  
                                                from emp where
                                                dept_no = @DeptNo";

                        var reader = consulta.ExecuteReader();
                        while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int emp_no = reader.GetInt32(reader.GetOrdinal("EMP_NO"));
                            string cognom = reader.GetString(reader.GetOrdinal("COGNOM"));

                            Emp e = new Emp(emp_no, cognom);

                            llista.Add(e);
                        }
                    }
                }
            }
            return llista;

        }

    }
}
