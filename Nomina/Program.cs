using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using DevExpress.Xpo;
using DevExpress.Data;
using DevExpress.Xpo.DB;

namespace Nomina
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string connectionString = ConfigurationManager.ConnectionStrings["CadenaConeccion"].ConnectionString;
            XpoDefault.DataLayer =
                XpoDefault.GetDataLayer(connectionString,
                AutoCreateOption.DatabaseAndSchema);
            Application.Run(new Principal());
        }
    }
}
