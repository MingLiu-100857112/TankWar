using System;
using System.Windows.Forms;

namespace WorldEditor
{
    static class Program
    {
        /// <summary>
        /// Entry of Main Program
        /// </summary>
        /// STAThreadAttribute indicates that the COM threading model for the application is single-threaded apartment. This attribute must be present on the entry point of any application that uses Windows Forms; if it is omitted, the Windows components might not work correctly. If the attribute is not present, the application uses the multithreaded apartment model, which is not supported for Windows Forms.
        [STAThread]
        static void Main()
        {
            /// <summary>
            /// Graphic double buffer process
            /// </summary>
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}