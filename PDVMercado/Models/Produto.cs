namespace PDVMercado.Models
{
    public class Produto
    {
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        
        // Preços
        public decimal PrecoVenda { get; set; }  // Era: Preco
        public decimal PrecoCusto { get; set; }  // Era: Custo
        
        // Estoque
        public int EstoqueAtual { get; set; }    // Era: Estoque
        public int EstoqueMinimo { get; set; }
        
        public string Unidade { get; set; } = "UN";  // Era: UnidadeMedida
        public string CodigoBarras { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        
        // Propriedades adicionais necessárias para Services
        public int Estoque  // Alias para compatibilidade
        {
            get => EstoqueAtual;
            set => EstoqueAtual = value;
        }
        public DateTime? DataAtualizacao { get; set; }
        
        // Métodos de validação
        public bool PodeSerVendido() => Ativo && EstoqueAtual > 0;

        public Produto()
        {
            Id = Guid.NewGuid().ToString();
            DataCadastro = DateTime.Now;
            DataAtualizacao = DateTime.Now;
            Ativo = true;
            EstoqueMinimo = 0;
            EstoqueAtual = 0;
        }

        public decimal CalcularLucro()
        {
            return PrecoVenda - PrecoCusto;
        }

        public decimal CalcularMargemLucro()
        {
            if (PrecoCusto <= 0) return 0;
            return ((PrecoVenda - PrecoCusto) / PrecoCusto) * 100;
        }
    }
}
