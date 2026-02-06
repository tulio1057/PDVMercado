
using PVDMercado.Models;
using SistemaMercado.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PDVMercado
{
    public partial class FechamentoForm : Form
    {
        // Propriedades
        private List<ItemVenda> _itensVenda;
        private decimal _totalVenda;

        // Construtor
        public FechamentoForm(List<ItemVenda> itensVenda)
        {
            InitializeComponent();

            // Guardar os itens da venda
            _itensVenda = itensVenda ?? new List<ItemVenda>();

            // Configurar form
            ConfigurarForm();
        }

        private void ConfigurarForm()
        {
            // 1. Configurar DataGridView
            ConfigurarDataGrid();

            // 2. Carregar itens no grid
            CarregarItensVenda();

            // 3. Calcular totais
            CalcularTotais();

            // 4. Configurar combobox de formas de pagamento
            ConfigurarFormasPagamento();

            // 5. Configurar eventos
            ConfigurarEventos();

            // 6. Focar no campo apropriado
            if (cmbFormaPagamento.Text == "Dinheiro")
                txtValorPago.Focus();
            else
                txtValorPago.SelectAll();
        }

        private void ConfigurarDataGrid()
        {
            // Limpar colunas existentes
            dgvResumo.Columns.Clear();

            // Configurar estilo do grid
            dgvResumo.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvResumo.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvResumo.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dgvResumo.EnableHeadersVisualStyles = false;
            dgvResumo.RowHeadersVisible = false;
            dgvResumo.AllowUserToAddRows = false;
            dgvResumo.AllowUserToDeleteRows = false;
            dgvResumo.ReadOnly = true;
            dgvResumo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResumo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResumo.DefaultCellStyle.Padding = new Padding(5);

            // Adicionar colunas
            var colunas = new[]
            {
                new { Name = "Produto", Header = "Produto", Width = 200 },
                new { Name = "Quantidade", Header = "Qtd", Width = 60 },
                new { Name = "Unitario", Header = "Preço Unit.", Width = 90 },
                new { Name = "Desconto", Header = "Desc.", Width = 70 },
                new { Name = "Total", Header = "Total", Width = 90 }
            };

            foreach (var col in colunas)
            {
                var dataColumn = new DataGridViewTextBoxColumn
                {
                    Name = col.Name,
                    HeaderText = col.Header,
                    Width = col.Width
                };

                // Formatação numérica para colunas de valor
                if (col.Name == "Unitario" || col.Name == "Desconto" || col.Name == "Total")
                {
                    dataColumn.DefaultCellStyle.Format = "C2";
                    dataColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Centralizar quantidade
                if (col.Name == "Quantidade")
                {
                    dataColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                dgvResumo.Columns.Add(dataColumn);
            }
        }

        private void CarregarItensVenda()
        {
            dgvResumo.Rows.Clear();

            foreach (var item in _itensVenda)
            {
                dgvResumo.Rows.Add(
                    item.ProdutoNome,
                    item.Quantidade,
                    item.PrecoUnitario,
                    item.Desconto,
                    item.TotalComDesconto
                );
            }

            // Ajustar altura das linhas
            dgvResumo.RowTemplate.Height = 30;
        }

        private void CalcularTotais()
        {
            // Calcular total da venda
            _totalVenda = 0;
            foreach (var item in _itensVenda)
            {
                _totalVenda += item.TotalComDesconto;
            }

            // Atualizar label
            lblTotalVenda.Text = _totalVenda.ToString("C2");

            // Se não for dinheiro, preencher valor pago automaticamente
            if (cmbFormaPagamento.Text != "Dinheiro" && cmbFormaPagamento.Text != "")
            {
                txtValorPago.Text = _totalVenda.ToString("N2");
                CalcularTroco();
            }
        }

        private void ConfigurarFormasPagamento()
        {
            cmbFormaPagamento.Items.Clear();

            // Formas de pagamento disponíveis
            string[] formas = {
                "Dinheiro",
                "Cartão de Crédito",
                "Cartão de Débito",
                "PIX",
                "Vale Alimentação",
                "Vale Refeição"
            };

            foreach (var forma in formas)
            {
                cmbFormaPagamento.Items.Add(forma);
            }

            // Selecionar a primeira por padrão
            if (cmbFormaPagamento.Items.Count > 0)
                cmbFormaPagamento.SelectedIndex = 0;
        }

        private void ConfigurarEventos()
        {
            // Evento do combobox
            cmbFormaPagamento.SelectedIndexChanged += CmbFormaPagamento_SelectedIndexChanged;

            // Evento do textbox de valor pago
            txtValorPago.TextChanged += TxtValorPago_TextChanged;
            txtValorPago.KeyPress += TxtValorPago_KeyPress;

            // Evento dos botões
            btnConfirmar.Click += BtnConfirmar_Click;
            btnCancelar.Click += BtnCancelar_Click;

            // Evento de tecla Enter no form
            this.KeyPreview = true;
            this.KeyDown += FechamentoForm_KeyDown;
        }

        // ============= EVENTOS =============

        private void CmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string formaSelecionada = cmbFormaPagamento.Text;

            if (formaSelecionada == "Dinheiro")
            {
                // Habilitar campo de valor pago
                txtValorPago.Enabled = true;
                txtValorPago.Clear();
                txtValorPago.Focus();
                txtValorPago.BackColor = Color.White;

                // Resetar troco
                lblTroco.Text = "R$ 0,00";
                lblTroco.ForeColor = Color.FromArgb(220, 53, 69);
            }
            else
            {
                // Desabilitar campo de valor pago
                txtValorPago.Enabled = false;
                txtValorPago.Text = _totalVenda.ToString("N2");
                txtValorPago.BackColor = Color.FromArgb(240, 240, 240);

                // Calcular troco (será zero)
                CalcularTroco();

                // Habilitar botão de confirmar
                btnConfirmar.Enabled = true;
            }
        }

        private void TxtValorPago_TextChanged(object sender, EventArgs e)
        {
            CalcularTroco();
        }

        private void CalcularTroco()
        {
            try
            {
                // Converter texto para decimal
                decimal valorPago = 0;
                if (!string.IsNullOrWhiteSpace(txtValorPago.Text))
                {
                    // Substituir vírgula por ponto se necessário
                    string valorTexto = txtValorPago.Text.Replace(",", ".");
                    valorPago = Convert.ToDecimal(valorTexto);
                }

                // Calcular troco
                decimal troco = valorPago - _totalVenda;

                // Atualizar label de troco
                if (troco >= 0)
                {
                    lblTroco.Text = troco.ToString("C2");
                    lblTroco.ForeColor = Color.FromArgb(40, 167, 69); // Verde

                    // Habilitar botão se valor for suficiente
                    btnConfirmar.Enabled = (cmbFormaPagamento.Text != "Dinheiro") ||
                                          (valorPago >= _totalVenda);
                }
                else
                {
                    lblTroco.Text = "Valor insuficiente!";
                    lblTroco.ForeColor = Color.FromArgb(220, 53, 69); // Vermelho
                    btnConfirmar.Enabled = false;
                }
            }
            catch (FormatException)
            {
                // Valor inválido
                lblTroco.Text = "Valor inválido";
                lblTroco.ForeColor = Color.FromArgb(220, 53, 69);
                btnConfirmar.Enabled = false;
            }
        }

        private void TxtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir apenas: números, backspace, delete, tab, vírgula e ponto
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != ',' &&
                e.KeyChar != '.')
            {
                e.Handled = true;
                return;
            }

            // Permitir apenas uma vírgula ou ponto
            if ((e.KeyChar == ',' || e.KeyChar == '.') &&
                txtValorPago.Text.Contains(",") || txtValorPago.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Tecla Enter confirma a venda
            if (e.KeyChar == (char)Keys.Enter && btnConfirmar.Enabled)
            {
                e.Handled = true;
                btnConfirmar.PerformClick();
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar dados
                if (!ValidarDados())
                    return;

                // Desabilitar botão para evitar duplo clique
                btnConfirmar.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // Criar objeto venda
                var venda = new Venda
                {
                    NumeroNota = GerarNumeroNota(),
                    DataVenda = DateTime.Now,
                    Itens = _itensVenda,
                    ValorTotal = _totalVenda,
                    FormaPagamento = cmbFormaPagamento.Text,
                    Status = "Pago",
                    DataPagamento = DateTime.Now,
                };

                // Calcular valor pago e troco
                if (cmbFormaPagamento.Text == "Dinheiro")
                {
                    decimal valorPago = Convert.ToDecimal(txtValorPago.Text.Replace(",", "."));
                    venda.ValorPago = valorPago;
                    venda.Troco = valorPago - _totalVenda;
                }
                else
                {
                    venda.ValorPago = _totalVenda;
                    venda.Troco = 0;
                }

                // TODO: Aqui você salvaria no Firebase
                // await _vendaService.SalvarVendaAsync(venda);

                // Gerar nota fiscal
                try
                {
                    // GeradorNotaFiscal.Gerar(venda);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao gerar nota: {ex.Message}");
                }

                // Mostrar mensagem de sucesso
                MessageBox.Show(
                    "✅ Venda finalizada com sucesso!\n\n" +
                    $"Número da Nota: {venda.NumeroNota}\n" +
                    $"Total: {venda.ValorTotal:C2}\n" +
                    $"Forma de Pagamento: {venda.FormaPagamento}\n" +
                    $"Troco: {venda.Troco:C2}",
                    "Venda Concluída",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Fechar form com sucesso
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao finalizar venda: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnConfirmar.Enabled = true;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidarDados()
        {
            // Validar forma de pagamento
            if (string.IsNullOrWhiteSpace(cmbFormaPagamento.Text))
            {
                MessageBox.Show("Selecione uma forma de pagamento!",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbFormaPagamento.Focus();
                return false;
            }

            // Validar valor pago se for dinheiro
            if (cmbFormaPagamento.Text == "Dinheiro")
            {
                if (string.IsNullOrWhiteSpace(txtValorPago.Text))
                {
                    MessageBox.Show("Informe o valor pago!",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValorPago.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtValorPago.Text.Replace(",", "."), out decimal valorPago))
                {
                    MessageBox.Show("Valor pago inválido!",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValorPago.SelectAll();
                    txtValorPago.Focus();
                    return false;
                }

                if (valorPago < _totalVenda)
                {
                    MessageBox.Show("Valor pago é menor que o total da venda!",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtValorPago.SelectAll();
                    txtValorPago.Focus();
                    return false;
                }
            }

            return true;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar o fechamento desta venda?",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void FechamentoForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Atalhos de teclado
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btnCancelar.PerformClick();
                    e.Handled = true;
                    break;

                case Keys.F2:
                    if (btnConfirmar.Enabled)
                        btnConfirmar.PerformClick();
                    e.Handled = true;
                    break;

                case Keys.F1:
                    cmbFormaPagamento.Focus();
                    e.Handled = true;
                    break;
            }
        }

        // ============= MÉTODOS AUXILIARES =============

        private string GerarNumeroNota()
        {
            // Gerar número de nota baseado na data e hora
            return DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        // Evento Load do Form
        private void FechamentoForm_Load(object sender, EventArgs e)
        {
            // Centralizar form na tela
            this.CenterToScreen();
        }
    }
}