using System.Drawing;

namespace PDVMercado.Utils
{
    public static class Cores
    {
        // Cores Principais
        public static Color Primary = Color.FromArgb(0, 123, 255);
        public static Color Secondary = Color.FromArgb(108, 117, 125);
        public static Color Success = Color.FromArgb(40, 167, 69);
        public static Color Danger = Color.FromArgb(220, 53, 69);
        public static Color Warning = Color.FromArgb(255, 193, 7);
        public static Color Info = Color.FromArgb(23, 162, 184);
        public static Color Light = Color.FromArgb(248, 249, 250);
        public static Color Dark = Color.FromArgb(52, 58, 64);

        // Cores de Texto
        public static Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static Color TextSecondary = Color.FromArgb(108, 117, 125);
        public static Color TextMuted = Color.FromArgb(134, 142, 150);

        // Cores de Fundo
        public static Color Background = Color.FromArgb(233, 236, 239);
        public static Color BackgroundLight = Color.White;
        public static Color BackgroundDark = Color.FromArgb(52, 58, 64);

        // Tons de Cinza
        public static Color LightGray = Color.FromArgb(222, 226, 230);
        public static Color MediumGray = Color.FromArgb(173, 181, 189);
        public static Color DarkGray = Color.FromArgb(73, 80, 87);

        // Cores de Estado
        public static Color Ativo = Color.FromArgb(40, 167, 69);
        public static Color Inativo = Color.FromArgb(220, 53, 69);
        public static Color Pendente = Color.FromArgb(255, 193, 7);
    }
}
