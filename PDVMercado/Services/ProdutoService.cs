using PDVMercado.Data;
using PDVMercado.Models;
using Google.Cloud.Firestore;

namespace PDVMercado.Services
{
    public class ProdutoService
    {
        private readonly FirestoreDb _firestoreDb;

        public ProdutoService()
        {
            _firestoreDb = FirebaseContext.Instance.FirestoreDb;
        }

        public async Task<List<Produto>> ListarProdutosAtivosAsync()
        {
            var collection = _firestoreDb.Collection("produtos");
            var query = collection.WhereEqualTo("Ativo", true);
            var snapshot = await query.GetSnapshotAsync();

            var produtos = new List<Produto>();
            foreach (var doc in snapshot.Documents)
            {
                var produto = doc.ConvertTo<Produto>();
                produto.Id = doc.Id;
                produtos.Add(produto);
            }

            return produtos.OrderBy(p => p.Nome).ToList();
        }

        public async Task<Produto> BuscarPorCodigoAsync(string codigo)
        {
            var collection = _firestoreDb.Collection("produtos");
            var query = collection.WhereEqualTo("Codigo", codigo)
                                 .WhereEqualTo("Ativo", true);
            var snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return null;

            var doc = snapshot.Documents[0];
            var produto = doc.ConvertTo<Produto>();
            produto.Id = doc.Id;

            return produto;
        }

        public async Task<Produto> BuscarPorIdAsync(string id)
        {
            var docRef = _firestoreDb.Collection("produtos").Document(id);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return null;

            var produto = snapshot.ConvertTo<Produto>();
            produto.Id = snapshot.Id;

            return produto;
        }

        public async Task<bool> AtualizarEstoqueAsync(string produtoId, int quantidade)
        {
            try
            {
                var produto = await BuscarPorIdAsync(produtoId);
                if (produto == null)
                    return false;

                produto.Estoque += quantidade;
                produto.DataAtualizacao = DateTime.Now;

                var docRef = _firestoreDb.Collection("produtos").Document(produtoId);
                await docRef.SetAsync(produto, SetOptions.MergeAll);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AdicionarProdutoAsync(Produto produto)
        {
            try
            {
                produto.DataCadastro = DateTime.Now;
                produto.Ativo = true;

                var collection = _firestoreDb.Collection("produtos");
                await collection.AddAsync(produto);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AtualizarProdutoAsync(Produto produto)
        {
            try
            {
                produto.DataAtualizacao = DateTime.Now;

                var docRef = _firestoreDb.Collection("produtos").Document(produto.Id);
                await docRef.SetAsync(produto, SetOptions.MergeAll);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}