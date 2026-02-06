using Google.Cloud.Firestore;

namespace PVDMercado.Models
{
    [FirestoreData]
    public class ItemVenda
    {
        [FirestoreProperty]
        public string ProdutoId { get; set; }

        [FirestoreProperty]
        public string ProdutoCodigo { get; set; }

        [FirestoreProperty]
        public string ProdutoNome { get; set; }

        [FirestoreProperty]
        public decimal PrecoUnitario { get; set; }

        [FirestoreProperty]
        public int Quantidade { get; set; }

        [FirestoreProperty]
        public decimal Total => PrecoUnitario * Quantidade;

        [FirestoreProperty]
        public decimal Desconto { get; set; }

        public decimal TotalComDesconto => Total - Desconto;
    }
}