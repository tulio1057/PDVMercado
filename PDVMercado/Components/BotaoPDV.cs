using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDVMercado.Components
{
    public class BotaoPDV : Button
    {
        public enum TipoBotao
        {
            Padrao,
            Sucesso,
            Perigo,
            Aviso,
            Info
        }

        private TipoBotao _tipo = TipoBotao.Padrao;

        public TipoBotao Tipo
        {
            get { return _tipo; }
            set
            {
                _tipo = value;
                AplicarEstilo();
            }
        }

        public BotaoPDV()
        {
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Cursor = Cursors.Hand;
            FlatAppearance.BorderSize = 0;
            AplicarEstilo();
        }

        private void AplicarEstilo()
        {
            switch (_tipo)
            {
                case TipoBotao.Sucesso:
                    BackColor = Color.FromArgb(40, 167, 69);
                    ForeColor = Color.White;
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(33, 136, 56);
                    break;

                case TipoBotao.Perigo:
                    BackColor = Color.FromArgb(220, 53, 69);
                    ForeColor = Color.White;
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 35, 51);
                    break;

                case TipoBotao.Aviso:
                    BackColor = Color.FromArgb(255, 193, 7);
                    ForeColor = Color.FromArgb(33, 37, 41);
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(235, 173, 0);
                    break;

                case TipoBotao.Info:
                    BackColor = Color.FromArgb(23, 162, 184);
                    ForeColor = Color.White;
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(19, 132, 150);
                    break;

                default: // Padrao
                    BackColor = Color.FromArgb(108, 117, 125);
                    ForeColor = Color.White;
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(90, 98, 104);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
        }
    }
}
