using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using PDVMercado.Models;
using PDVMercado.Utils;

namespace PDVMercado.Forms
{
    public partial class CaixaForm
    {
        #region Inicialização de Dados
        private void InicializarDados()
        {
            _configuracao = new Configuracao
            {
                NomeEmpresa = "Mercado Demo",
                CNPJ = "00.000.000/0000-00",
                Endereco = "Rua Demo, 123",
                Telefone = "(00) 0000-0000",
                ImprimirAutomatico = false,
                AbrirGavetaAutomatico = false,
                PermitirVendaSemEstoque = true,
                PortaImpressora = "COM1",
                PortaGaveta = "COM1"
            };

            CriarProdutosExemplo();
            NovaVenda();
        }

        private void CriarProdutosExemplo()
        {
            _produtosCache = new List<Produto>
            {
                new Produto { Id = "1", Codigo = "001", Nome = "Arroz Tipo 1 - 5kg", PrecoVenda = 25.90m, PrecoCusto = 18.00m, EstoqueAtual = 50, EstoqueMinimo = 10, Categoria = "Alimentos", Unidade = "UN", Ativo = true },
                new Produto { Id = "2", Codigo = "002", Nome = "Feijão Preto - 1kg", PrecoVenda = 8.50m, PrecoCusto = 6.00m, EstoqueAtual = 30, EstoqueMinimo = 10, Categoria = "Alimentos", Unidade = "UN", Ativo = true },
                new Produto { Id = "3", Codigo = "003", Nome = "Óleo de Soja - 900ml", PrecoVenda = 7.90m, PrecoCusto = 5.50m, EstoqueAtual = 40, EstoqueMinimo = 15, Categoria = "Alimentos", Unidade = "UN", Ativo = true },
                new Produto { Id = "4", Codigo = "004", Nome = "Açúcar Cristal - 1kg", PrecoVenda = 4.50m, PrecoCusto = 3.20m, EstoqueAtual = 60, EstoqueMinimo = 20, Categoria = "Alimentos", Unidade = "UN", Ativo = true },
                new Produto { Id = "5", Codigo = "005", Nome = "Café Torrado - 500g", PrecoVenda = 12.90m, PrecoCusto = 9.00m, EstoqueAtual = 25, EstoqueMinimo = 10, Categoria = "Alimentos", Unidade = "UN", Ativo = true },
                new Produto { Id = "6", Codigo = "006", Nome = "Leite Integral - 1L", PrecoVenda = 5.90m, PrecoCusto = 4.20m, EstoqueAtual = 35, EstoqueMinimo = 15, Categoria = "Bebidas", Unidade = "UN", Ativo = true },
                new Produto { Id = "7", Codigo = "007", Nome = "Refrigerante Cola - 2L", PrecoVenda = 8.90m, PrecoCusto = 6.50m, EstoqueAtual = 45, EstoqueMinimo = 20, Categoria = "Bebidas", Unidade = "UN", Ativo = true },
                new Produto { Id = "8", Codigo = "008", Nome = "Detergente Líquido - 500ml", PrecoVenda = 2.50m, PrecoCusto = 1.80m, EstoqueAtual = 50, EstoqueMinimo = 20, Categoria = "Limpeza", Unidade = "UN", Ativo = true },
                new Produto { Id = "9", Codigo = "009", Nome = "Sabonete 90g", PrecoVenda = 1.99m, PrecoCusto = 1.20m, EstoqueAtual = 100, EstoqueMinimo = 30, Categoria = "Higiene", Unidade = "UN", Ativo = true },
                new Produto { Id = "10", Codigo = "010", Nome = "Pão Francês", PrecoVenda = 0.50m, PrecoCusto = 0.30m, EstoqueAtual = 200, EstoqueMinimo = 50, Categoria = "Padaria", Unidade = "UN", Ativo = true }
            };

            PreencherGridProdutos(_produtosCache);
        }
        #endregion

        #region Lógica de Produtos
        private void AtualizarProdutos()
        {
            PreencherGridProdutos(_produtosCache);
            SystemSounds.Asterisk.Play();
            Mensagens.Info("Lista de produtos atualizada!");
        }

        private void PreencherGridProdutos(List<Produto> produtos)
        {
            dgvProdutos.Rows.Clear();
            
            foreach (var produto in produtos)
            {
                dgvProdutos.Rows.Add(
                    produto.Codigo,
                    produto.Nome,
                    produto.PrecoVenda.ToString("C2")
                );
            }
        }

        private void FiltrarProdutos()
        {
            if (_produtosCache == null) return;

            string filtro = txtPesquisa.Text.ToLower();
            
            if (string.IsNullOrWhiteSpace(filtro))
            {
                PreencherGridProdutos(_produtosCache);
                return;
            }

            var produtosFiltrados = _produtosCache
                .Where(p => 
                    p.Codigo.ToLower().Contains(filtro) ||
                    p.Nome.ToLower().Contains(filtro) ||
                    (p.CodigoBarras != null && p.CodigoBarras.ToLower().Contains(filtro))
                )
                .ToList();

            PreencherGridProdutos(produtosFiltrados);
        }

        private void AdicionarProdutoPorCodigo()
        {
            string codigo = txtCodigo.Text.Trim();
            
            if (string.IsNullOrEmpty(codigo))
            {
                SystemSounds.Beep.Play();
                return;
            }

            var produto = _produtosCache.FirstOrDefault(p => 
                p.Codigo == codigo || p.CodigoBarras == codigo);

            if (produto != null)
            {
                if (!produto.Ativo)
                {
                    SystemSounds.Hand.Play();
                    Mensagens.Aviso("Produto inativo!");
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                if (!_configuracao.PermitirVendaSemEstoque && produto.EstoqueAtual <= 0)
                {
                    SystemSounds.Hand.Play();
                    Mensagens.Aviso("Produto sem estoque!");
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                AdicionarItemVenda(produto);
                txtCodigo.Clear();
                txtCodigo.Focus();
            }
            else
            {
                SystemSounds.Hand.Play();
                Mensagens.Aviso($"Produto '{codigo}' não encontrado!");
                txtCodigo.SelectAll();
                txtCodigo.Focus();
            }
        }
        #endregion

        #region Lógica de Venda
        private void NovaVenda()
        {
            if (_vendaAtual != null && _vendaAtual.Itens.Count > 0)
            {
                if (!Mensagens.Confirmar("Deseja cancelar a venda atual e iniciar uma nova?"))
                    return;
            }

            _vendaAtual = new Venda
            {
                UsuarioId = SessaoUsuario.UsuarioAtual?.Id ?? "admin",
                NomeUsuario = SessaoUsuario.UsuarioAtual?.Nome ?? "Administrador"
            };

            dgvItensVenda.Rows.Clear();
            AtualizarTotais();
            txtCodigo.Clear();
            txtCodigo.Focus();
            
            SystemSounds.Asterisk.Play();
        }

        private void AdicionarItemVenda(Produto produto, decimal quantidade = 1)
        {
            if (_vendaAtual == null)
                NovaVenda();

            var item = new ItemVenda(produto, quantidade);
            _vendaAtual?.AdicionarItem(item); // ✅ CORRIGIDO: Null-conditional operator

            AtualizarGridVenda();
            AtualizarTotais();

            SystemSounds.Beep.Play();
            
            if (dgvItensVenda.Rows.Count > 0)
            {
                dgvItensVenda.FirstDisplayedScrollingRowIndex = dgvItensVenda.Rows.Count - 1;
                dgvItensVenda.Rows[dgvItensVenda.Rows.Count - 1].Selected = true;
            }
        }

        private void AtualizarGridVenda()
        {
            dgvItensVenda.Rows.Clear();

            if (_vendaAtual == null) return;

            foreach (var item in _vendaAtual.Itens)
            {
                dgvItensVenda.Rows.Add(
                    item.CodigoProduto,
                    item.NomeProduto,
                    item.Quantidade.ToString("N2"),
                    item.PrecoUnitario.ToString("C2"),
                    item.Subtotal.ToString("C2")
                );
            }
        }

        private void AtualizarTotais()
        {
            if (_vendaAtual == null || _vendaAtual.Itens.Count == 0)
            {
                lblTotalItens.Text = "Itens: 0";
                lblValorTotal.Text = "R$ 0,00";
                return;
            }

            lblTotalItens.Text = $"Itens: {_vendaAtual.QuantidadeItens}"; // ✅ CORRIGIDO: Removido () - é propriedade
            lblValorTotal.Text = _vendaAtual.Total.ToString("C2");
        }

        private void RemoverItemSelecionado()
        {
            if (dgvItensVenda.SelectedRows.Count == 0)
            {
                Mensagens.Aviso("Selecione um item para remover!");
                return;
            }

            if (!Mensagens.Confirmar("Deseja remover o item selecionado?"))
                return;

            int index = dgvItensVenda.SelectedRows[0].Index;
            
            if (_vendaAtual != null && index < _vendaAtual.Itens.Count)
            {
                var item = _vendaAtual.Itens[index];
                _vendaAtual.RemoverItem(item); // ✅ CORRIGIDO: Passar o objeto ItemVenda, não o Id
                
                AtualizarGridVenda();
                AtualizarTotais();
                
                SystemSounds.Asterisk.Play();
            }
        }

        private void AlterarQuantidadeItem(int rowIndex)
        {
            if (_vendaAtual == null || rowIndex >= _vendaAtual.Itens.Count)
                return;

            var item = _vendaAtual.Itens[rowIndex];
            
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Digite a nova quantidade para '{item.NomeProduto}':",
                "Alterar Quantidade",
                item.Quantidade.ToString("N2")
            );

            if (string.IsNullOrWhiteSpace(input))
                return;

            if (decimal.TryParse(input.Replace(",", "."), out decimal novaQuantidade))
            {
                if (novaQuantidade <= 0)
                {
                    Mensagens.Aviso("Quantidade deve ser maior que zero!");
                    return;
                }

                item.AlterarQuantidade(novaQuantidade);
                _vendaAtual.CalcularTotais();
                
                AtualizarGridVenda();
                AtualizarTotais();
                
                SystemSounds.Asterisk.Play();
            }
            else
            {
                Mensagens.Erro("Quantidade inválida!");
            }
        }

        private void FinalizarVenda()
        {
            if (_vendaAtual == null || _vendaAtual.Itens.Count == 0)
            {
                Mensagens.Aviso("Não há itens na venda!");
                return;
            }

            // ✅ CORRIGIDO: FechamentoForm recebe a venda e o total
            FechamentoForm fechamento = new FechamentoForm(_vendaAtual, _vendaAtual.Total);
            
            if (fechamento.ShowDialog() == DialogResult.OK)
            {
                SystemSounds.Asterisk.Play();
                Mensagens.Sucesso($"Venda finalizada com sucesso!\n\nTotal: {_vendaAtual.Total:C2}\nForma de Pagamento: {_vendaAtual.FormaPagamento}");
                
                NovaVenda();
            }
        }

        private void CancelarVenda()
        {
            if (_vendaAtual == null || _vendaAtual.Itens.Count == 0)
            {
                NovaVenda();
                return;
            }

            if (Mensagens.Confirmar("Deseja cancelar a venda atual?"))
            {
                NovaVenda();
                SystemSounds.Asterisk.Play();
            }
        }

        private FormaPagamento ObterFormaPagamentoSelecionada()
        {
            if (rbDinheiro.Checked) return FormaPagamento.Dinheiro;
            if (rbCartao.Checked) return FormaPagamento.CartaoCredito;
            if (rbPix.Checked) return FormaPagamento.Pix;
            return FormaPagamento.Dinheiro;
        }
        #endregion

        #region Métodos Auxiliares
        private void FocarPesquisa()
        {
            txtPesquisa.Focus();
            txtPesquisa.SelectAll();
        }

        private void FecharSistema()
        {
            if (Mensagens.Confirmar("Deseja sair do sistema?"))
            {
                Application.Exit();
            }
        }

        private void MostrarAtalhos()
        {
            string atalhos = @"ATALHOS DO SISTEMA

F1 - Ajuda
F2 - Nova Venda
F3 - Pesquisar Produto
F5 - Atualizar Lista de Produtos
F9 - Finalizar Venda
ESC - Cancelar Venda

DICA: Use o campo 'Código' e pressione ENTER para adicionar produtos rapidamente!";

            MessageBox.Show(atalhos, "Atalhos do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarSobre()
        {
            string sobre = @"PDV MERCADO - Sistema de Ponto de Venda
Versão 1.0.0

Desenvolvido em C# WinForms
© 2025 - Todos os direitos reservados

Recursos:
• Cadastro de produtos
• Controle de estoque
• Vendas rápidas
• Multi-usuários

Credenciais de Acesso:
Usuário: admin
Senha: 1234";

            MessageBox.Show(sobre, "Sobre o Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AbrirCadastroProduto()
        {
            CadastroForm cadastro = new CadastroForm();
            cadastro.ShowDialog();
        }
        #endregion
    }
}
