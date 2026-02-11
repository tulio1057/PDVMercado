using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using PDVMercado.Components;
using PDVMercado.Models;
using PDVMercado.Utils;

namespace PDVMercado.Forms
{
    public partial class LoginForm : Form
    {
        #region Campos e Propriedades
        private TextBox txtUsuario;
        private TextBox txtSenha;
        private BotaoPDV btnEntrar;
        private BotaoPDV btnSair;
        private Label lblTitulo;
        private Label lblUsuario;
        private Label lblSenha;
        private Label lblMensagem;
        private Panel panelLogin;
        private Panel panelCabecalho;
        #endregion

        #region Construtor e Inicialização
        public LoginForm()
        {
            InitializeComponentCustom();
            ConfigurarEventos();
        }

        private void InitializeComponentCustom()
        {
            // Configurar Form
            Text = "PDV Mercado - Login";
            Size = new Size(450, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Cores.Background;
            Icon = SystemIcons.Application;

            // Painel Cabeçalho
            panelCabecalho = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(450, 120),
                BackColor = Cores.Primary,
                Dock = DockStyle.Top
            };

            // Título
            lblTitulo = new Label
            {
                Text = "PDV MERCADO",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(0, 30),
                Size = new Size(450, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblSubtitulo = new Label
            {
                Text = "Sistema de Ponto de Venda",
                Font = Fontes.Cabecalho,
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(0, 75),
                Size = new Size(450, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            panelCabecalho.Controls.Add(lblTitulo);
            panelCabecalho.Controls.Add(lblSubtitulo);

            // Painel de Login
            panelLogin = new Panel
            {
                Location = new Point(50, 150),
                Size = new Size(350, 300),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Label Usuário
            lblUsuario = new Label
            {
                Text = "Usuário:",
                Font = Fontes.Cabecalho,
                Location = new Point(30, 40),
                Size = new Size(290, 25),
                ForeColor = Cores.DarkGray
            };

            // TextBox Usuário
            txtUsuario = new TextBox
            {
                Location = new Point(30, 70),
                Size = new Size(290, 30),
                Font = Fontes.Padrao,
                TabIndex = 0
            };

            // Label Senha
            lblSenha = new Label
            {
                Text = "Senha:",
                Font = Fontes.Cabecalho,
                Location = new Point(30, 120),
                Size = new Size(290, 25),
                ForeColor = Cores.DarkGray
            };

            // TextBox Senha
            txtSenha = new TextBox
            {
                Location = new Point(30, 150),
                Size = new Size(290, 30),
                Font = Fontes.Padrao,
                PasswordChar = '•',
                TabIndex = 1
            };

            // Label Mensagem
            lblMensagem = new Label
            {
                Location = new Point(30, 190),
                Size = new Size(290, 20),
                Font = new Font("Segoe UI", 8F),
                ForeColor = Cores.Danger,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            // Botão Entrar
            btnEntrar = new BotaoPDV
            {
                Text = "ENTRAR",
                Location = new Point(30, 230),
                Size = new Size(140, 45),
                Tipo = BotaoPDV.TipoBotao.Sucesso,
                TabIndex = 2
            };

            // Botão Sair
            btnSair = new BotaoPDV
            {
                Text = "SAIR",
                Location = new Point(180, 230),
                Size = new Size(140, 45),
                Tipo = BotaoPDV.TipoBotao.Perigo,
                TabIndex = 3
            };

            // Adicionar controles ao painel de login
            panelLogin.Controls.Add(lblUsuario);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(lblSenha);
            panelLogin.Controls.Add(txtSenha);
            panelLogin.Controls.Add(lblMensagem);
            panelLogin.Controls.Add(btnEntrar);
            panelLogin.Controls.Add(btnSair);

            // Adicionar ao form
            Controls.Add(panelLogin);
            Controls.Add(panelCabecalho);

            // Label de versão
            Label lblVersao = new Label
            {
                Text = "Versão 1.0.0",
                Font = new Font("Segoe UI", 7F),
                ForeColor = Cores.MediumGray,
                Location = new Point(0, 480),
                Size = new Size(450, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblVersao);

            // Label de credenciais (para desenvolvimento)
            Label lblCredenciais = new Label
            {
                Text = "Usuário: admin | Senha: 1234",
                Font = new Font("Segoe UI", 7F, FontStyle.Italic),
                ForeColor = Cores.Info,
                Location = new Point(0, 460),
                Size = new Size(450, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lblCredenciais);
        }

        private void ConfigurarEventos()
        {
            btnEntrar.Click += BtnEntrar_Click;
            btnSair.Click += BtnSair_Click;
            txtSenha.KeyPress += TxtSenha_KeyPress;
            txtUsuario.KeyPress += TxtUsuario_KeyPress;
            Load += LoginForm_Load;
        }
        #endregion

        #region Event Handlers
        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtSenha.Focus();
            }
        }

        private void TxtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                RealizarLogin();
            }
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            RealizarLogin();
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Lógica de Negócio
        private void RealizarLogin()
        {
            // Validações
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarErro("Digite o usuário!");
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MostrarErro("Digite a senha!");
                txtSenha.Focus();
                return;
            }

            // AUTENTICAÇÃO SIMPLES POR CÓDIGO
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;

            // Verificar credenciais
            if (usuario.ToLower() == "admin" && senha == "1234")
            {
                // Login bem-sucedido
                SystemSounds.Asterisk.Play();
                
                // Criar usuário na sessão
                Usuario usuarioLogado = new Usuario
                {
                    Id = "admin",
                    Login = "admin",
                    Nome = "Administrador",
                    Email = "admin@pdvmercado.com",
                    Tipo = TipoUsuario.Administrador,
                    Ativo = true
                };

                SessaoUsuario.IniciarSessao(usuarioLogado);
                
                lblMensagem.Text = "Login realizado com sucesso!";
                lblMensagem.ForeColor = Cores.Success;
                lblMensagem.Visible = true;

                // Aguardar um momento antes de abrir a tela principal
                System.Threading.Thread.Sleep(500);
                
                // Abrir tela principal
                Hide();
                CaixaForm caixa = new CaixaForm();
                caixa.FormClosed += (s, args) => Close();
                caixa.Show();
            }
            else
            {
                // Login falhou
                SystemSounds.Hand.Play();
                MostrarErro("Usuário ou senha inválidos!");
                txtSenha.Clear();
                txtUsuario.Focus();
                txtUsuario.SelectAll();
            }
        }

        private void MostrarErro(string mensagem)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.ForeColor = Cores.Danger;
            lblMensagem.Visible = true;
        }
        #endregion
    }
}
