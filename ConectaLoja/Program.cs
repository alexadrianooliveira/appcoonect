using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConectaLoja
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Logar());
            LogarLoja();
        }

        private static void LogarLoja()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BLL.Security valida = new BLL.Security();
            Models.Loja loja = valida.CarregaDadosSalvos();
            int retorno = 0;
            if (loja.Email != "")
                retorno = valida.checkStore(loja.Email, loja.Senha);

            if (retorno == 0)
                Application.Run(new Logar());
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
