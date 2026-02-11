namespace PDVMercado.Models
{
    public class ItemVenda
    {
        public string Id { get; set; } = string.Empty;
        public string ProdutoId { get; set; } = string.Empty;
        public string CodigoProduto { get; set; } = string.Empty;
        public string NomeProduto { get; set; } = string.Empty;
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal Subtotal => (Quantidade * PrecoUnitario) - Desconto;
        public decimal Total => (Quantidade * PrecoUnitario) - Desconto;
        
        // Aliases para compatibilidade
        public string ProdutoNome => NomeProduto;
        public string ProdutoCodigo => CodigoProduto;

        public ItemVenda()
        {
            Id = Guid.NewGuid().ToString();
            Quantidade = 1;
            Desconto = 0;
        }

        public ItemVenda(Produto produto, decimal quantidade)
        {
            Id = Guid.NewGuid().ToString();
            ProdutoId = produto.Id;
            CodigoProduto = produto.Codigo;
            NomeProduto = produto.Nome;
            Quantidade = quantidade;
            PrecoUnitario = produto.PrecoVenda;
            Desconto = 0;
        }

        public void AlterarQuantidade(decimal novaQuantidade)
        {
            if (novaQuantidade > 0)
            {
                Quantidade = novaQuantidade;
            }
        }
    }
}
