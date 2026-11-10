using System;
using System.Windows.Forms;

namespace Conception_SAV
{
  internal class Program
  {
    [STAThread]
    static void Main()
    {
      DatabaseInitializer.Initialize();

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      Application.Run(new FormConnexion());
    }
  }
}
