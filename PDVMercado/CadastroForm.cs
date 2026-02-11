using System;
using System.Windows.Forms;
using PDVMercado.Models;

namespace PDVMercado
{
    public partial class CadastroForm : Form
    {
        public CadastroForm()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.Text = "Cadastro de Produtos";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(600, 500);

            // Aqui você adiciona os controles do formulário
            // Labels, TextBoxes, Buttons, DataGridView, etc.
        }

        // ShowDialog já é herdado de Form, então não precisa implementar
        // Mas você pode criar métodos auxiliares:

        private void SalvarProduto()
        {
            // Lógica para salvar produto
        }

        private void LimparCampos()
        {
            // Lógica para limpar os campos do formulário
        }

        private void FecharFormulario()
        {
            this.Close();
        }
    }
}
