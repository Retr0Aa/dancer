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
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();

            MainApp app = new MainApp();
            app.Run();

            Application.Run(app);
        }
    }
}
