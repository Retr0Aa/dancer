using Dancer.Framework;
using Dancer.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dancer
{
    public class Program
    {
        public static Retr0Log.Logger logger;

        static void Main()
        {
            logger = new Retr0Log.Logger("APP", new Retr0Log.LogSettings(true, true, true));

            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();

            MainApp app = new MainApp();
            app.Run();

            Application.Run(app);
        }
    }
}
