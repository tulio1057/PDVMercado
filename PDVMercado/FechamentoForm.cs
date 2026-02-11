using System;
using System.Windows.Forms;
using PDVMercado.Models;
using System.Linq;

namespace PDVMercado
{
    public partial class FechamentoForm : Form
    {
        private Venda _vendaAtual;
        private decimal _totalVenda;

        public FormaPagamento FormaPagamentoSelecionado { get; private set; }
        public decimal ValorPago { get; private set; }
        public decimal Troco { get; private set; }

        public FechamentoForm(Venda venda, decimal total)
        {
            InitializeComponent();
            _vendaAtual = venda;
            _totalVenda = total;
            
            ConfigurarEventos();
            CarregarDados();
        }

        private void CarregarDados()
        {
            lblTotalValor.Text = _totalVenda.ToString("C2");
            
            // Simular formas de pagamento (poderia vir de um serviço)
            cmbFormaPagamento.Items.Add("Dinheiro");
            cmbFormaPagamento.Items.Add("Cartão de Crédito");
            cmbFormaPagamento.Items.Add("Cartão de Débito");
            cmbFormaPagamento.Items.Add("PIX");
            
            cmbFormaPagamento.SelectedIndex = 0;
            txtValorPago.Text = _totalVenda.ToString("N2");
            CalcularTroco();
        }

        private void ConfigurarEventos()
        {
            btnConfirmar.Click += (s, e) => FinalizarPagamento();
            btnCancelar.Click += (s, e) => CancelarPagamento();
            
            txtValorPago.TextChanged += (s, e) => CalcularTroco();
            txtValorPago.KeyPress += TxtValorPago_KeyPress;
            
            this.KeyDown += FechamentoForm_KeyDown;
        }

        private void TxtValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // Apenas uma vírgula
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                FinalizarPagamento();
            }
        }

        private void FechamentoForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                FinalizarPagamento();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                CancelarPagamento();
            }
        }

        private void CalcularTroco()
        {
            if (decimal.TryParse(txtValorPago.Text, out decimal valorPago))
            {
                decimal troco = valorPago - _totalVenda;
                lblTrocoValor.Text = troco > 0 ? troco.ToString("C2") : "R$ 0,00";
                Troco = troco > 0 ? troco : 0;
                ValorPago = valorPago;
            }
            else
            {
                lblTrocoValor.Text = "R$ 0,00";
                Troco = 0;
                ValorPago = 0;
            }
        }

        private void FinalizarPagamento()
        {
            if (decimal.TryParse(txtValorPago.Text, out decimal valorPago))
            {
                if (valorPago < _totalVenda)
                {
                    MessageBox.Show("O valor pago é insuficiente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ValorPago = valorPago;
                // Aqui você definiria o objeto FormaPagamento real se tivesse uma lista de modelos
                // Por simplicidade, vamos apenas usar o texto por enquanto ou criar um objeto simples
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Informe um valor pago válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarPagamento()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
