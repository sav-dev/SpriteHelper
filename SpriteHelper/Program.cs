using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            switch (Defaults.Instance.AppToRun)
            {
                case "Animation":
                    Application.Run(new AnimationHelper());
                    break;

                case "Background":
                    Application.Run(new BackgroundTilesetCreator());
                    break;

                case "Level":
                    Application.Run(new LevelEditor());
                    break;
            }
        }
    }
}
