using System;
using System.Windows.Forms;

namespace SatelliteEntry
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SatelliteEntry satelliteEntryForm = new SatelliteEntry();
            if (satelliteEntryForm.IsInitialized)
                Application.Run(satelliteEntryForm);
        }
    }
}
