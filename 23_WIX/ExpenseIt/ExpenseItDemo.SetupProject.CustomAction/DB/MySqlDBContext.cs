
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.Entity;

namespace ExpenseItDemo.SetupProject.CustomAction.DB
{
    public class MySqlDBContext : DbContext
    {
        string host;
        int port;
        string db;
        string login;
        string password;
        
        public MySqlDBContext(string host, int port, string db, string login, string password ) {
                this.host = host;
                this.port = port;
                this.db = db;
                this.login = login;
                this.password = password;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

                optionBuilder.UseMySQL(@"Uid="+login+";Pwd="+password+";port="+port+";Host="+host+";Database="+db+";");

        }
    }

}

