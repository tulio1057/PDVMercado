using System;
using System.Windows.Forms;

namespace PDVMercado
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Login simples para teste
            if (txtUsuario.Text == "admin" && txtSenha.Text == "123")
            {
                MessageBox.Show("Login bem-sucedido!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir tela principal
                CaixaForm caixaForm = new CaixaForm();
                caixaForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}