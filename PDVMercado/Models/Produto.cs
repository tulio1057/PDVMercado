using Google.Cloud.Firestore;

namespace SistemaMercado.Models
{
    [FirestoreData]
    public class Produto
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Codigo { get; set; }

        [FirestoreProperty]
        public string Nome { get; set; }

        [FirestoreProperty]
        public string Descricao { get; set; }

        [FirestoreProperty]
        public decimal Preco { get; set; }

        [FirestoreProperty]
        public decimal Custo { get; set; }

        [FirestoreProperty]
        public int Estoque { get; set; }

        [FirestoreProperty]
        public bool Ativo { get; set; }

        [FirestoreProperty]
        public DateTime DataCadastro { get; set; }

        [FirestoreProperty]
        public DateTime? DataAtualizacao { get; set; }

        [FirestoreProperty]
        public string Categoria { get; set; }

        [FirestoreProperty]
        public string UnidadeMedida { get; set; }

        public bool PodeSerVendido => Ativo && Estoque > 0;

        public void DecrementarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("Quantidade deve ser maior que zero");

            if (Estoque < quantidade)
                throw new InvalidOperationException("Estoque insuficiente");

            Estoque -= quantidade;
            DataAtualizacao = DateTime.Now;
        }
    }
}