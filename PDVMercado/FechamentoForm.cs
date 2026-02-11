using System;
using System.Windows.Forms;
using PDVMercado.Models;

namespace PDVMercado
{
    public partial class FechamentoForm : Form
    {
        private Venda _vendaAtual;
        private decimal _totalVenda;

        // Construtor com 2 parâmetros (conforme esperado pelo CaixaForm_Logic)
        public FechamentoForm(Venda venda, decimal total)
        {
            InitializeComponent();
            _vendaAtual = venda;
            _totalVenda = total;
            ConfigurarFormulario();
        }

        // Propriedades públicas para retornar os dados
        public FormaPagamento FormaPagamentoSelecionado { get; private set; }
        public decimal ValorPago { get; private set; }
        public decimal Troco { get; private set; }

        private void ConfigurarFormulario()
        {
            this.Text = "Finalizar Venda";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(500, 400);

            // Aqui você adiciona os controles do formulário
            // Labels, TextBoxes, Buttons, etc.
        }

        // Este método será chamado quando o usuário confirmar o pagamento
        private void FinalizarPagamento()
        {
            // Validar os dados
            // Calcular troco se for dinheiro
            // Definir FormaPagamentoSelecionado, ValorPago, Troco
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Este método será chamado quando o usuário cancelar
        private void CancelarPagamento()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
