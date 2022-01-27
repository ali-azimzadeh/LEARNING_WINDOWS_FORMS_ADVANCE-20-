using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEARNING_WINDOWS_FORMS_ADVANCE_20_
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

            try
            {

                Taskbar oTaskbar = new Taskbar();

                SplashForm splashForm = new SplashForm();

                if (splashForm.Height > (ScreenArea.MaximumHeight - oTaskbar.Size.Height))
                {
                    splashForm.Location =
                        new Point(splashForm.Height - ScreenArea.MaximumHeight);
                }

                Application.Run(splashForm);

                //LoginForm loginForm = new LoginForm();

                //Application.Run(loginForm);

                //if (loginForm.IsDisposed == false)
                //{
                //    loginForm.Dispose();
                //}

                //MainForm mainForm = new MainForm();

                //Application.Run(mainForm);

                //if (mainForm.IsDisposed == false)
                //{
                //    mainForm.Dispose();
                //}

            }

            catch (System.Exception ex)
            {
                //Log ex

                //Application.Exit();
                Application.Restart();
            }

        }
    }
}
