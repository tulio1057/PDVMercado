using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using PDVMercado.Components;
using PDVMercado.Models;
using PDVMercado.Utils;

namespace PDVMercado.Forms
{
    public partial class CaixaForm : Form
    {
        #region Campos e Propriedades
        private MenuStrip menuPrincipal;
        private Panel panelCabecalho;
        private Panel panelEntradaDados;
        private Panel panelVendaAtual;
        private Panel panelAcoes;
        private Panel panelRodape;

        private Label lblLogo;
        private Label lblUsuario;
        private Label lblDataHora;
        private System.Windows.Forms.Timer timerRelogio;

        private TextBox txtCodigo;
        private TextBox txtPesquisa;
        private DataGridView dgvProdutos;
        private DataGridView dgvItensVenda;

        private Label lblTotalItens;
        private Label lblTotalVenda;
        private Label lblValorTotal;

        private BotaoPDV btnFinalizarVenda;
        private BotaoPDV btnCancelarVenda;
        private BotaoPDV btnPesquisarProduto;
        private BotaoPDV btnRemoverItem;

        private RadioButton rbDinheiro;
        private RadioButton rbCartao;
        private RadioButton rbPix;

        private Venda _vendaAtual;
        private List<Produto> _produtosCache;
        private Configuracao _configuracao;
        #endregion

        #region Construtor e Inicialização
        public CaixaForm()
        {
            InitializeComponentCustom();
            ConfigurarEventos();
            InicializarDados();
        }

        private void InitializeComponentCustom()
        {
            SuspendLayout();

            // Configurar Form
            Text = "PDV Mercado - Caixa";
            Size = new Size(1400, 800);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Cores.Background;
            WindowState = FormWindowState.Maximized;

            CriarMenu();
            CriarCabecalho();
            CriarPainelEntradaDados();
            CriarPainelVendaAtual();
            CriarPainelAcoes();
            CriarRodape();

            ResumeLayout();
        }

        private void CriarMenu()
        {
            menuPrincipal = new MenuStrip
            {
                BackColor = Cores.Primary,
                ForeColor = Color.White,
                Font = Fontes.Padrao
            };

            // Menu Arquivo
            var menuArquivo = new ToolStripMenuItem("Arquivo");
            menuArquivo.DropDownItems.Add("Nova Venda", null, (s, e) => NovaVenda());
            menuArquivo.DropDownItems.Add(new ToolStripSeparator());
            menuArquivo.DropDownItems.Add("Sair", null, (s, e) => FecharSistema());

            // Menu Venda
            var menuVenda = new ToolStripMenuItem("Venda");
            menuVenda.DropDownItems.Add("Finalizar (F9)", null, (s, e) => FinalizarVenda());
            menuVenda.DropDownItems.Add("Cancelar (ESC)", null, (s, e) => CancelarVenda());
            menuVenda.DropDownItems.Add(new ToolStripSeparator());
            menuVenda.DropDownItems.Add("Remover Item", null, (s, e) => RemoverItemSelecionado());

            // Menu Produtos
            var menuProdutos = new ToolStripMenuItem("Produtos");
            menuProdutos.DropDownItems.Add("Pesquisar (F3)", null, (s, e) => FocarPesquisa());
            menuProdutos.DropDownItems.Add("Cadastrar Novo", null, (s, e) => AbrirCadastroProduto());
            menuProdutos.DropDownItems.Add("Atualizar Lista (F5)", null, (s, e) => AtualizarProdutos());

            // Menu Ajuda
            var menuAjuda = new ToolStripMenuItem("Ajuda");
            menuAjuda.DropDownItems.Add("Atalhos", null, (s, e) => MostrarAtalhos());
            menuAjuda.DropDownItems.Add("Sobre", null, (s, e) => MostrarSobre());

            menuPrincipal.Items.Add(menuArquivo);
            menuPrincipal.Items.Add(menuVenda);
            menuPrincipal.Items.Add(menuProdutos);
            menuPrincipal.Items.Add(menuAjuda);

            Controls.Add(menuPrincipal);
        }

        private void CriarCabecalho()
        {
            panelCabecalho = new Panel
            {
                Location = new Point(0, 24),
                Size = new Size(1400, 60),
                BackColor = Cores.Primary,
                Dock = DockStyle.Top
            };

            lblLogo = new Label
            {
                Text = "🛒 PDV MERCADO",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblUsuario = new Label
            {
                Text = $"👤 {SessaoUsuario.ObterNomeUsuario()}",
                Font = Fontes.Cabecalho,
                ForeColor = Color.White,
                Location = new Point(900, 20),
                Size = new Size(250, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            lblDataHora = new Label
            {
                Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Font = Fontes.Padrao,
                ForeColor = Color.White,
                Location = new Point(1180, 20),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.MiddleRight
            };

            timerRelogio = new System.Windows.Forms.Timer { Interval = 1000 };
            timerRelogio.Tick += (s, e) => lblDataHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timerRelogio.Start();

            panelCabecalho.Controls.Add(lblLogo);
            panelCabecalho.Controls.Add(lblUsuario);
            panelCabecalho.Controls.Add(lblDataHora);

            Controls.Add(panelCabecalho);
        }

        private void CriarPainelEntradaDados()
        {
            panelEntradaDados = new Panel
            {
                Location = new Point(10, 95),
                Size = new Size(450, 600),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitulo = new Label
            {
                Text = "ENTRADA DE DADOS",
                Font = Fontes.Cabecalho,
                BackColor = Cores.Primary,
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(448, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblCodigo = new Label
            {
                Text = "Código do Produto:",
                Font = Fontes.Padrao,
                Location = new Point(15, 50),
                Size = new Size(200, 20)
            };

            txtCodigo = new TextBox
            {
                Location = new Point(15, 75),
                Size = new Size(418, 30),
                Font = new Font("Consolas", 12F),
                TabIndex = 0
            };

            Label lblPesquisa = new Label
            {
                Text = "Pesquisar Produto:",
                Font = Fontes.Padrao,
                Location = new Point(15, 120),
                Size = new Size(200, 20)
            };

            txtPesquisa = new TextBox
            {
                Location = new Point(15, 145),
                Size = new Size(418, 30),
                Font = Fontes.Padrao
            };

            dgvProdutos = new DataGridView
            {
                Location = new Point(15, 185),
                Size = new Size(418, 400),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = Fontes.Padrao
            };

            dgvProdutos.Columns.Add("Codigo", "Código");
            dgvProdutos.Columns.Add("Nome", "Nome");
            dgvProdutos.Columns.Add("Preco", "Preço");
            dgvProdutos.Columns[0].Width = 80;
            dgvProdutos.Columns[1].Width = 220;
            dgvProdutos.Columns[2].Width = 100;

            panelEntradaDados.Controls.Add(lblTitulo);
            panelEntradaDados.Controls.Add(lblCodigo);
            panelEntradaDados.Controls.Add(txtCodigo);
            panelEntradaDados.Controls.Add(lblPesquisa);
            panelEntradaDados.Controls.Add(txtPesquisa);
            panelEntradaDados.Controls.Add(dgvProdutos);

            Controls.Add(panelEntradaDados);
        }

        private void CriarPainelVendaAtual()
        {
            panelVendaAtual = new Panel
            {
                Location = new Point(470, 95),
                Size = new Size(700, 600),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitulo = new Label
            {
                Text = "VENDA ATUAL",
                Font = Fontes.Cabecalho,
                BackColor = Cores.Success,
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(698, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            dgvItensVenda = new DataGridView
            {
                Location = new Point(10, 45),
                Size = new Size(678, 450),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Consolas", 10F)
            };

            dgvItensVenda.Columns.Add("Codigo", "Código");
            dgvItensVenda.Columns.Add("Nome", "Produto");
            dgvItensVenda.Columns.Add("Quantidade", "Qtd");
            dgvItensVenda.Columns.Add("PrecoUnitario", "Preço Un.");
            dgvItensVenda.Columns.Add("Subtotal", "Subtotal");

            dgvItensVenda.Columns[0].Width = 80;
            dgvItensVenda.Columns[1].Width = 280;
            dgvItensVenda.Columns[2].Width = 80;
            dgvItensVenda.Columns[3].Width = 100;
            dgvItensVenda.Columns[4].Width = 120;

            Panel panelTotais = new Panel
            {
                Location = new Point(10, 505),
                Size = new Size(678, 85),
                BackColor = Cores.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTotalItens = new Label
            {
                Text = "Itens: 0",
                Font = Fontes.Cabecalho,
                Location = new Point(20, 15),
                Size = new Size(200, 25)
            };

            lblTotalVenda = new Label
            {
                Text = "TOTAL:",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Location = new Point(20, 45),
                Size = new Size(200, 30)
            };

            lblValorTotal = new Label
            {
                Text = "R$ 0,00",
                Font = new Font("Consolas", 20F, FontStyle.Bold),
                ForeColor = Cores.Success,
                Location = new Point(460, 15),
                Size = new Size(200, 40),
                TextAlign = ContentAlignment.MiddleRight
            };

            panelTotais.Controls.Add(lblTotalItens);
            panelTotais.Controls.Add(lblTotalVenda);
            panelTotais.Controls.Add(lblValorTotal);

            panelVendaAtual.Controls.Add(lblTitulo);
            panelVendaAtual.Controls.Add(dgvItensVenda);
            panelVendaAtual.Controls.Add(panelTotais);

            Controls.Add(panelVendaAtual);
        }

        private void CriarPainelAcoes()
        {
            panelAcoes = new Panel
            {
                Location = new Point(1180, 95),
                Size = new Size(200, 600),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblTitulo = new Label
            {
                Text = "AÇÕES",
                Font = Fontes.Cabecalho,
                BackColor = Cores.Primary,
                ForeColor = Color.White,
                Location = new Point(0, 0),
                Size = new Size(198, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnFinalizarVenda = new BotaoPDV
            {
                Text = "FINALIZAR\nVENDA",
                Location = new Point(10, 50),
                Size = new Size(178, 60),
                Tipo = BotaoPDV.TipoBotao.Sucesso,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            btnCancelarVenda = new BotaoPDV
            {
                Text = "CANCELAR\nVENDA",
                Location = new Point(10, 120),
                Size = new Size(178, 60),
                Tipo = BotaoPDV.TipoBotao.Perigo,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            btnRemoverItem = new BotaoPDV
            {
                Text = "REMOVER\nITEM",
                Location = new Point(10, 190),
                Size = new Size(178, 50),
                Tipo = BotaoPDV.TipoBotao.Aviso
            };

            btnPesquisarProduto = new BotaoPDV
            {
                Text = "PESQUISAR\nPRODUTO",
                Location = new Point(10, 250),
                Size = new Size(178, 50),
                Tipo = BotaoPDV.TipoBotao.Info
            };

            Label lblSeparador = new Label
            {
                Text = "━━━━━━━━━━━━━━━━━━━━━",
                Font = Fontes.Padrao,
                ForeColor = Cores.MediumGray,
                Location = new Point(10, 310),
                Size = new Size(178, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblFormaPgto = new Label
            {
                Text = "FORMA DE PAGAMENTO:",
                Font = Fontes.PadraoNegrito,
                Location = new Point(10, 340),
                Size = new Size(178, 20)
            };

            rbDinheiro = new RadioButton
            {
                Text = "💵 Dinheiro",
                Font = Fontes.Padrao,
                Location = new Point(20, 370),
                Size = new Size(160, 25),
                Checked = true
            };

            rbCartao = new RadioButton
            {
                Text = "💳 Cartão",
                Font = Fontes.Padrao,
                Location = new Point(20, 400),
                Size = new Size(160, 25)
            };

            rbPix = new RadioButton
            {
                Text = "📱 PIX",
                Font = Fontes.Padrao,
                Location = new Point(20, 430),
                Size = new Size(160, 25)
            };

            panelAcoes.Controls.Add(lblTitulo);
            panelAcoes.Controls.Add(btnFinalizarVenda);
            panelAcoes.Controls.Add(btnCancelarVenda);
            panelAcoes.Controls.Add(btnRemoverItem);
            panelAcoes.Controls.Add(btnPesquisarProduto);
            panelAcoes.Controls.Add(lblSeparador);
            panelAcoes.Controls.Add(lblFormaPgto);
            panelAcoes.Controls.Add(rbDinheiro);
            panelAcoes.Controls.Add(rbCartao);
            panelAcoes.Controls.Add(rbPix);

            Controls.Add(panelAcoes);
        }

        private void CriarRodape()
        {
            panelRodape = new Panel
            {
                Location = new Point(0, 705),
                Size = new Size(1400, 30),
                BackColor = Cores.DarkGray,
                Dock = DockStyle.Bottom
            };

            Label lblRodape = new Label
            {
                Text = "F1: Ajuda | F2: Nova Venda | F3: Pesquisar | F5: Atualizar | F9: Finalizar | ESC: Cancelar",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.White,
                Location = new Point(10, 7),
                Size = new Size(1380, 16),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panelRodape.Controls.Add(lblRodape);
            Controls.Add(panelRodape);
        }
        #endregion

        #region Configuração de Eventos
        private void ConfigurarEventos()
        {
            Load += CaixaForm_Load;
            KeyDown += CaixaForm_KeyDown;
            KeyPreview = true;

            txtCodigo.KeyPress += TxtCodigo_KeyPress;
            txtPesquisa.TextChanged += TxtPesquisa_TextChanged;
            dgvProdutos.CellDoubleClick += DgvProdutos_CellDoubleClick;
            dgvItensVenda.CellDoubleClick += DgvItensVenda_CellDoubleClick;

            btnFinalizarVenda.Click += (s, e) => FinalizarVenda();
            btnCancelarVenda.Click += (s, e) => CancelarVenda();
            btnRemoverItem.Click += (s, e) => RemoverItemSelecionado();
            btnPesquisarProduto.Click += (s, e) => FocarPesquisa();

            FormClosing += CaixaForm_FormClosing;
        }

        private void CaixaForm_Load(object sender, EventArgs e)
        {
            txtCodigo.Focus();
        }

        private void CaixaForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    MostrarAtalhos();
                    break;
                case Keys.F2:
                    NovaVenda();
                    break;
                case Keys.F3:
                    FocarPesquisa();
                    break;
                case Keys.F5:
                    AtualizarProdutos();
                    break;
                case Keys.F9:
                    FinalizarVenda();
                    break;
                case Keys.Escape:
                    CancelarVenda();
                    break;
            }
        }

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                AdicionarProdutoPorCodigo();
            }
        }

        private void TxtPesquisa_TextChanged(object sender, EventArgs e)
        {
            FiltrarProdutos();
        }

        private void DgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var codigo = dgvProdutos.Rows[e.RowIndex].Cells["Codigo"].Value?.ToString();
                if (!string.IsNullOrEmpty(codigo))
                {
                    txtCodigo.Text = codigo;
                    AdicionarProdutoPorCodigo();
                }
            }
        }

        private void DgvItensVenda_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AlterarQuantidadeItem(e.RowIndex);
            }
        }

        private void CaixaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_vendaAtual != null && _vendaAtual.Itens.Count > 0)
            {
                if (!Mensagens.Confirmar("Existe uma venda em andamento. Deseja realmente sair?"))
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        // Continua no próximo arquivo...
    }
}
