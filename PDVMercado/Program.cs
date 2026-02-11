using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Windows.Forms;
using PDVMercado.Forms; // ✅ ADICIONADO: Namespace do LoginForm

namespace PDVMercado
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; } = null!; // ✅ CORRIGIDO: Inicialização nula

        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
