using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Saraff.Twain.CapSample {

    internal static class Program {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
