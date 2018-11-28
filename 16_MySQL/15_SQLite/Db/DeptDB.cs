using _15_SQLite.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace _15_SQLite.Db
{
    class DeptDB
    {

        /// <summary>
        /// Funció que retorna un llistat de departaments
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Dept> GetDepts( int DeptNoFiltre = -1, string dNomFiltre = "")
        {
            ObservableCollection<Dept> llista = new ObservableCollection<Dept>();

            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using(var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();

                    // Crear una consulta SQL
                    using( var consulta = connexio.CreateCommand())
                    {
                        //------------------------------------------
                        //---    Creació de paràmetres
                        //------------------------------------------

                        DBUtils.CrearParametre("DeptNoParam", DeptNoFiltre, consulta);
                        DBUtils.CrearParametre("DnomParam", "%"+dNomFiltre+"%", consulta);
                        
                        //------------------------------------------

                        consulta.CommandText = $@"select *  
                                                from dept where
                                                ( @DeptNoParam<0  or dept_no = @DeptNoParam ) and  
                                                ( @DnomParam = '' or dnom like @DnomParam )";

                        var reader = consulta.ExecuteReader();
                        while(reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                        {
                            int dept_no = reader.GetInt32(reader.GetOrdinal("DEPT_NO"));
                            string dnom = reader.GetString(reader.GetOrdinal("DNOM"));
                            string loc = reader.GetString(reader.GetOrdinal("LOC"));


                            Dept d = new Dept(dept_no, dnom, loc);

                            llista.Add(d);
                        }
                    }
                }
            }
            return llista;

        }

        internal static int GetDeptsAmbNom(int deptNo, string nom)
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
                        DBUtils.CrearParametre("p_dept_no", deptNo, consulta);
                        DBUtils.CrearParametre("p_nom", nom, consulta);
                        // query SQL
                        consulta.CommandText = @"select count(*)  from dept 
                                                where 
                                                    dept_no<>@p_dept_no and
                                                    upper(dnom) = upper(@p_nom) ";
                        return Convert.ToInt32(consulta.ExecuteScalar());
                    }
                }
            }
        }

        /*
        internal delegate int ScalarFunction(params object[] parameters);

        internal static int magicfunction(ScalarFunction funcio)
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
                        return funcio.Invoke(consulta);
                    }
                }
            }
        }*/



     

        internal static int GetNumeroEmpleats(int deptNo)
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
                        DBUtils.CrearParametre("p_dept_no", deptNo, consulta);
                        // query SQL
                        consulta.CommandText = @"select count(*)  from emp where dept_no=@p_dept_no ";
                        return Convert.ToInt32(consulta.ExecuteScalar());
                    }
                }
            }


        }

        internal static void ActualitzaDepartament(int id, string txbNom, string txbLoc)
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

                        DBUtils.CrearParametre("IdParam", id, consulta);
                        DBUtils.CrearParametre("NomParam",  txbNom , consulta);
                        DBUtils.CrearParametre("LocParam",  txbLoc , consulta);

                        // query SQL
                        consulta.CommandText = @"update dept set dnom=@NomParam, loc=@LocParam where dept_no = @IdParam ";

                        int actualitzades = consulta.ExecuteNonQuery();
                        if(actualitzades!=1)
                        {
                            throw new Exception("PUFFFFF");
                        }
                    }
                }
            }
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


        public static Dept InsertDept( string pNom, string pLoc )
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                using (var connexio = context.Database.GetDbConnection()) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                {
                    // Obrir la connexió a la BD
                    connexio.Open();
                    DbTransaction transaccio = connexio.BeginTransaction();
                    // Crear una consulta SQL
                    using (var consulta = connexio.CreateCommand())
                    {

                        consulta.CommandText = "update ids set last_id=last_id+1 where table_name='dept'";
                        // això assegurea que la consulta es faci dins de la transacció engegada anteriorment.
                        consulta.Transaction = transaccio;
                        int mod = consulta.ExecuteNonQuery();
                        if (mod != 1) throw new Exception("");
                        //--------------------------------------------------------------------------------------
                        consulta.CommandText = "select last_id from ids where table_name='dept'";
                        Int32 last_id = Convert.ToInt32(consulta.ExecuteScalar());
                        //--------------------------------------------------------------------------------------

                        DBUtils.CrearParametre("idparam",  last_id, consulta);
                        DBUtils.CrearParametre("nomparam", pNom,    consulta);
                        DBUtils.CrearParametre("locparam", pLoc,    consulta);

                        consulta.CommandText = "insert into dept (dept_no, dnom, loc) values ( @idparam, @nomparam, @locparam )";
                        int filesInserides = consulta.ExecuteNonQuery();
                        if (filesInserides != 1) throw new Exception("");

                        // desem els canvis de tota la transacció
                        transaccio.Commit();
                        // Creem un object de departament nou.
                        return new Dept(last_id, pNom, pLoc);
                    }
                }
            }

        }

        internal static void esborrarDepartament(int id)
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

                        DbTransaction transaccio = connexio.BeginTransaction();

                        DBUtils.CrearParametre("IdParam", id, consulta);
                  
                        // query SQL
                        consulta.CommandText = @"delete from dept where dept_no = @IdParam ";
                        consulta.Transaction = transaccio;

                        int actualitzades = consulta.ExecuteNonQuery();
                        if (actualitzades != 1)
                        {
                            transaccio.Rollback(); // Keep yourself safe and warm.
                            throw new Exception("PUFFFFF");
                        }
                        else
                        {
                            transaccio.Commit(); // ou yeah!
                        }
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
