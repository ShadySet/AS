using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using AS.Lib.Logging;

namespace AS.TaskAgent
{
    static class Program
    {

        public static Alog log = new Alog("AS.TaskAgent Main");
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.Debug("Started");
            Mutex appSingleton = new Mutex(false, "AS.TaskAgent Single Instance");
            if (appSingleton.WaitOne(0, false))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                log.Debug("Running application");
                Application.Run(new AgentForm());
                appSingleton.Close();
            }
            else
            {
                log.Debug("Another app instance was detected. Closing the application");
                MessageBox.Show("Sorry, only one instance of TaskAgent can be ran at once.");
            }
            log.Debug("Exit");
        }
    }
}
