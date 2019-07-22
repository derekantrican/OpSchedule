using System;
using System.IO;
using System.Windows.Forms;

namespace OpSchedule
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

            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += Application_ThreadException;

            // Set the unhandled exception mode to force all Windows Forms errors
            // to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Common.CheckRequiredDirectoriesExist();
            Common.LogFilePath = Path.Combine(Common.LogPath, "Log (" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + ").log");
            Common.LogContents = "";
            Common.Log("Program initializing... (v" + Common.ApplicationVersion + ")");

            Application.Run(new Main());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
                Common.DumpException(e.ExceptionObject as Exception);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Common.DumpException(e.Exception);
        }
    }
}
