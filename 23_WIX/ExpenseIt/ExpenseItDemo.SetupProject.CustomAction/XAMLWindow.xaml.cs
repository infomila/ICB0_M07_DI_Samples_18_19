using ExpenseItDemo.SetupProject.CustomAction.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExpenseItDemo.SetupProject.CustomAction
{
    /// <summary>
    /// Lógica de interacción para XAMLWindow.xaml
    /// </summary>
    public partial class XAMLWindow : Window
    {
        public bool succesful = false;

        public XAMLWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {

            bool ok = runScript();
            if(!ok)
            {
                MessageBox.Show("Error de connexió");
            } else
            {
                MessageBox.Show("BD creada correctament");
                this.Close();
            }
        }

        private String[] getDDLQueries()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            String resourceName = "ExpenseItDemo.SetupProject.CustomAction.script.sql";

            string sqlString = "x";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                sqlString = reader.ReadToEnd();
            }
            return sqlString.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private bool runScript() {


            int port;
            if(!Int32.TryParse(txbPort.Text, out port)) {
                port = 3306;
            }

            try
            {
                using (MySqlDBContext context =
                    new MySqlDBContext(txbServer.Text, port, txbDB.Text, txbUser.Text, txbPassword.Text))
                {
                    using (var connexio = context.Database.Connection) // <== NOTA IMPORTANT: requereix ==>using Microsoft.EntityFrameworkCore;
                    {
                        // Obrir la connexió a la BD

                        connexio.Open();

                        // Crear una consulta SQL
                        using (var consulta = connexio.CreateCommand())
                        {
                            String[] queries = getDDLQueries();
                            //------------------------------------------
                            for (int i = 0; i < queries.Length; i++)
                            {
                                consulta.CommandText = queries[i];
                                consulta.ExecuteNonQuery();
                            }
                            return true;
                        }
                    }
                }
            }catch(Exception ex)
            {
                return false;
            }
            
        }




        public bool isSuccessful()
        {
            return succesful;
        }

     
    }
}
