using SpriteHelper.Dialogs;
using System;
using System.Windows.Forms;

namespace SpriteHelper
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

            bool run = true;
            while (run)
            {
                try
                {
                    Application.Run(new ProgramPicker());
                    run = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    run = true;
                }
            }
        }
    }
}
