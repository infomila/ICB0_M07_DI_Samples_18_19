using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Deployment.WindowsInstaller;

namespace ExpenseItDemo.SetupProject.CustomAction
{
    public class CustomActions
    {

        private static ManualResetEvent semafor = new ManualResetEvent(false);


        [CustomAction]
        public static ActionResult CustomActionMostrarFinestra(Session session)
        {
            session.Log("Begin CustomAction1");

            Thread t = new Thread(ObrirFinestra);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            semafor.WaitOne();
            return ActionResult.Success;
        }

        [STAThread]
        static void ObrirFinestra()
        {


            /*
             // Obrir finestra de Windows Forms
            FinestraConfiguracio fc = new FinestraConfiguracio();
            fc.ShowDialog();
            */
            //Obrir finestra de WFP (xaml)
            XAMLWindow w = new XAMLWindow();
            w.txbSQL.Text = sqlString;
            w.ShowDialog();
            
            semafor.Set();

            
        }


    }
}
