namespace PDVMercado.Models
{
    public enum StatusVenda
    {
        EmAndamento,
        Finalizada,
        Cancelada
    }

    public class Venda
    {
        public string Id { get; set; } = string.Empty;
        public int NumeroVenda { get; set; }
        public DateTime DataHora { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Troco { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public StatusVenda Status { get; set; }
        public string? Observacoes { get; set; }

        // Propriedades calculadas e aliases para compatibilidade
        public int QuantidadeItens => Itens?.Sum(i => (int)i.Quantidade) ?? 0;
        public int NumeroNota { get; set; }
        public DateTime DataVenda  // Alias para DataHora
        {
            get => DataHora;
            set => DataHora = value;
        }
        public string UsuarioNome => NomeUsuario;
        public decimal ValorTotal => Total;
        public DateTime? DataPagamento { get; set; }
        
        // Métodos de validação
        public bool EstaPaga() => Status == StatusVenda.Finalizada;
        public bool EstaCancelada() => Status == StatusVenda.Cancelada;

        public Venda()
        {
            Id = Guid.NewGuid().ToString();
            DataHora = DateTime.Now;
            Status = StatusVenda.EmAndamento;
            Itens = new List<ItemVenda>();
        }

        public void CalcularTotais()
        {
            Subtotal = Itens.Sum(i => i.Total);
            Total = Subtotal - Desconto;
        }

        public void AdicionarItem(ItemVenda item)
        {
            Itens.Add(item);
            CalcularTotais();
        }

        public void RemoverItem(ItemVenda item)
        {
            Itens.Remove(item);
            CalcularTotais();
        }
    }
}
