using Google.Cloud.Firestore;
using PVDMercado.Models;

namespace SistemaMercado.Models
{
    [FirestoreData]
    public class Venda
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string NumeroNota { get; set; }

        [FirestoreProperty]
        public string UsuarioId { get; set; }

        [FirestoreProperty]
        public string UsuarioNome { get; set; }

        [FirestoreProperty]
        public DateTime DataVenda { get; set; }

        [FirestoreProperty]
        public decimal ValorTotal { get; set; }

        [FirestoreProperty]
        public decimal ValorPago { get; set; }

        [FirestoreProperty]
        public decimal Troco { get; set; }

        [FirestoreProperty]
        public string FormaPagamento { get; set; } // "Dinheiro", "Cartão", "PIX"

        [FirestoreProperty]
        public string Status { get; set; } // "Pendente", "Pago", "Cancelado"

        [FirestoreProperty]
        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();

        [FirestoreProperty]
        public DateTime? DataPagamento { get; set; }

        [FirestoreProperty]
        public string Observacao { get; set; }

        public bool EstaPaga => Status == "Pago";
        public bool EstaCancelada => Status == "Cancelado";

        public void CalcularTotais()
        {
            ValorTotal = Itens.Sum(item => item.Total);
            if (ValorPago > 0)
                Troco = ValorPago - ValorTotal > 0 ? ValorPago - ValorTotal : 0;
        }
    }
}