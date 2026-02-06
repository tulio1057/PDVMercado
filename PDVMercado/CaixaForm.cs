using PVDMercado.Models;

namespace PDVMercado
{
    public partial class CaixaForm : Form
    {
        // Lista de itens da venda atual
        private List<ItemVenda> itensVenda = new List<ItemVenda>();

        public CaixaForm()
        {
            InitializeComponent();
            ConfigurarDataGrids();
            AtualizarTotais();
        }

        private void ConfigurarDataGrids()
        {
            // Configurar grid de produtos
            dgvProdutos.Columns.Add("Codigo", "Código");
            dgvProdutos.Columns.Add("Nome", "Produto");
            dgvProdutos.Columns.Add("Preco", "Preço");
            dgvProdutos.Columns.Add("Estoque", "Estoque");

            // Configurar grid de itens da venda
            dgvItens.Columns.Add("Produto", "Produto");
            dgvItens.Columns.Add("Qtd", "Qtd");
            dgvItens.Columns.Add("Unitario", "Unitário");
            dgvItens.Columns.Add("Total", "Total");

            // Adicionar dados de teste
            AdicionarProdutosTeste();
        }

        private void AdicionarProdutosTeste()
        {
            // Dados de teste - remova depois
            dgvProdutos.Rows.Add("001", "Arroz 5kg", 25.90, 50);
            dgvProdutos.Rows.Add("002", "Feijão 1kg", 8.50, 100);
            dgvProdutos.Rows.Add("003", "Açúcar 5kg", 18.75, 30);
            dgvProdutos.Rows.Add("004", "Óleo 900ml", 9.90, 80);
            dgvProdutos.Rows.Add("005", "Café 500g", 15.50, 60);
        }

        private void txtCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AdicionarItemPorCodigo(txtCodigoBarras.Text);
                txtCodigoBarras.Clear();
                e.Handled = true;
            }
        }

        private void AdicionarItemPorCodigo(string codigo)
        {
            // Buscar produto pelo código (simulação)
            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                if (row.Cells["Codigo"].Value?.ToString() == codigo)
                {
                    string nome = row.Cells["Nome"].Value.ToString();
                    decimal preco = Convert.ToDecimal(row.Cells["Preco"].Value);

                    // Adicionar à venda
                    var item = new ItemVenda
                    {
                        ProdutoCodigo = codigo,
                        ProdutoNome = nome,
                        PrecoUnitario = preco,
                        Quantidade = 1
                    };

                    itensVenda.Add(item);

                    // Adicionar ao grid
                    dgvItens.Rows.Add(nome, 1, preco.ToString("C2"), preco.ToString("C2"));

                    AtualizarTotais();
                    break;
                }
            }
        }

        private void AtualizarTotais()
        {
            decimal total = 0;
            int qtdItens = 0;

            foreach (var item in itensVenda)
            {
                total += item.Total;
                qtdItens += item.Quantidade;
            }

            lblTotal.Text = $"Total: R$ {total:N2}";
            lblQtdItens.Text = $"Itens: {qtdItens}";
        }

        private void dgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var codigo = dgvProdutos.Rows[e.RowIndex].Cells["Codigo"].Value?.ToString();
                if (!string.IsNullOrEmpty(codigo))
                {
                    AdicionarItemPorCodigo(codigo);
                }
            }
        }

        private void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            if (itensVenda.Count == 0)
            {
                MessageBox.Show("Adicione itens à venda!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var fechamentoForm = new FechamentoForm(itensVenda))
            {
                if (fechamentoForm.ShowDialog() == DialogResult.OK)
                {
                    // Limpar venda
                    itensVenda.Clear();
                    dgvItens.Rows.Clear();
                    AtualizarTotais();

                    MessageBox.Show("Venda finalizada com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            if (itensVenda.Count > 0)
            {
                var result = MessageBox.Show("Cancelar esta venda?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    itensVenda.Clear();
                    dgvItens.Rows.Clear();
                    AtualizarTotais();
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void novaVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCancelarVenda_Click(sender, e);
        }

        private void finalizarVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnFinalizarVenda_Click(sender, e);
        }
    }
}