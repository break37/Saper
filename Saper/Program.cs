using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saper.Model;
using Saper.View;
using Saper.Presenter;

namespace Saper
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

            IGameWindow view = new GameWindow();
            MainModel model = new MainModel();

            GameWindowPresenter presenter = new GameWindowPresenter(view, model);

            Application.Run((GameWindow)view);
        }
    }
}
