using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;

namespace ExpenseItDemo.SetupProject.CustomActions
{
    public class CustomActions
    {
        static ManualResetEvent semafor = new ManualResetEvent(false);


        [CustomAction]
        public static ActionResult CustomAction1(Session session)
        {
            session.Log("Begin CustomAction1");

            Thread t = new Thread(worker);
            // STA (Single Threaded Apartment)
            //---------------------------------------------------------------
            // The UI thread of a WPF or Windows Forms project should always 
            // be STA, as does any thread that creates a window.
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            semafor.WaitOne();
            return ActionResult.Success;
        }

        [STAThread]
        public static worker()
        {
            try
            {
                DBDialog c = new DBDialog();
                c.ShowDialog();
            }
            finally
            {
                semafor.Set();
            }
        }

    }
}
