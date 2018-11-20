﻿using _15_SQLite.Model;
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


        public static void InsertDept( string pNom, string pLoc )
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

                        consulta.CommandText = "update table ids set last_id=last_id+1 where table_name='dept'";
                        // això assegurea que la consulta es faci dins de la transacció engegada anteriorment.
                        consulta.Transaction = transaccio;
                        int mod = consulta.ExecuteNonQuery();
                        if (mod != 1) throw new Exception("");
                        //--------------------------------------------------------------------------------------
                        consulta.CommandText = "selec last_id from ids where table_name='dept'";
                        int last_id = (int)consulta.ExecuteScalar();
                        //--------------------------------------------------------------------------------------

                        DBUtils.CrearParametre("idparam",  last_id, consulta);
                        DBUtils.CrearParametre("nomparam", pNom,    consulta);
                        DBUtils.CrearParametre("locparam", pLoc,    consulta);

                        consulta.CommandText = "insert into ids (dept_no, dnom, loc) values ( @idparam, @nomparam, @locparam )";
                        int filesInserides = consulta.ExecuteNonQuery();
                        if (filesInserides != 1) throw new Exception("");

                        // desem els canvis de tota la transacció
                        transaccio.Commit();
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