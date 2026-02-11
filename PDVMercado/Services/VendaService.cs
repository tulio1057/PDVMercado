using PDVMercado.Data;
using PDVMercado.Models;
using Google.Cloud.Firestore;
using System.Transactions;

namespace PDVMercado.Services
{
    public class VendaService
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly ProdutoService _produtoService;

        public VendaService()
        {
            _firestoreDb = FirebaseContext.Instance.FirestoreDb;
            _produtoService = new ProdutoService();
        }

        public async Task<Venda> CriarVendaAsync(Venda venda)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                // Validar produtos e estoque
                foreach (var item in venda.Itens)
                {
                    var produto = await _produtoService.BuscarPorIdAsync(item.ProdutoId);
                    if (produto == null || !produto.PodeSerVendido()) // ✅ CORRIGIDO: Adicionado ()
                        throw new Exception($"Produto {item.ProdutoNome} não disponível");

                    if (produto.Estoque < item.Quantidade)
                        throw new Exception($"Estoque insuficiente para {item.ProdutoNome}");
                }

                // Atualizar estoque
                foreach (var item in venda.Itens)
                {
                    await _produtoService.AtualizarEstoqueAsync(item.ProdutoId, (int)-item.Quantidade); // ✅ CORRIGIDO: Conversão para int
                }

                // Gerar número da venda e nota
                venda.NumeroVenda = GerarNumeroSequencial();
                venda.NumeroNota = venda.NumeroVenda; // NumeroNota = NumeroVenda
                venda.DataVenda = DateTime.Now;
                venda.Status = StatusVenda.EmAndamento; // ✅ CORRIGIDO: Usar enum ao invés de string

                // Salvar venda
                var collection = _firestoreDb.Collection("vendas");
                var docRef = await collection.AddAsync(venda);
                venda.Id = docRef.Id;

                scope.Complete();
                return venda;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar venda: {ex.Message}");
            }
        }

        public async Task<bool> FinalizarVendaAsync(string vendaId, decimal valorPago, FormaPagamento formaPagamento) // ✅ CORRIGIDO: Tipo FormaPagamento
        {
            try
            {
                var docRef = _firestoreDb.Collection("vendas").Document(vendaId);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return false;

                var venda = snapshot.ConvertTo<Venda>();
                venda.Id = snapshot.Id;

                if (venda.EstaPaga() || venda.EstaCancelada()) // ✅ CORRIGIDO: Adicionado ()
                    return false;

                venda.ValorPago = valorPago;
                venda.FormaPagamento = formaPagamento; // ✅ CORRIGIDO: Agora é enum
                venda.Status = StatusVenda.Finalizada; // ✅ CORRIGIDO: Usar enum
                venda.DataPagamento = DateTime.Now;
                venda.CalcularTotais();

                await docRef.SetAsync(venda, SetOptions.MergeAll);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> CancelarVendaAsync(string vendaId)
        {
            try
            {
                var docRef = _firestoreDb.Collection("vendas").Document(vendaId);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return false;

                var venda = snapshot.ConvertTo<Venda>();
                venda.Id = snapshot.Id;

                if (venda.EstaPaga() || venda.EstaCancelada()) // ✅ CORRIGIDO: Adicionado ()
                    return false;

                // Restaurar estoque
                foreach (var item in venda.Itens)
                {
                    await _produtoService.AtualizarEstoqueAsync(item.ProdutoId, (int)item.Quantidade); // ✅ CORRIGIDO: Conversão para int
                }

                venda.Status = StatusVenda.Cancelada; // ✅ CORRIGIDO: Usar enum
                await docRef.SetAsync(venda, SetOptions.MergeAll);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Venda>> ListarVendasPorPeriodoAsync(DateTime inicio, DateTime fim)
        {
            var collection = _firestoreDb.Collection("vendas");
            var query = collection.WhereGreaterThanOrEqualTo("DataVenda", inicio)
                                 .WhereLessThanOrEqualTo("DataVenda", fim)
                                 .WhereEqualTo("Status", StatusVenda.Finalizada.ToString()); // ✅ CORRIGIDO: Conversão de enum para string

            var snapshot = await query.GetSnapshotAsync();

            var vendas = new List<Venda>();
            foreach (var doc in snapshot.Documents)
            {
                var venda = doc.ConvertTo<Venda>();
                venda.Id = doc.Id;
                vendas.Add(venda);
            }

            return vendas.OrderByDescending(v => v.DataVenda).ToList();
        }

        private int GerarNumeroSequencial()
        {
            // Gera número baseado em timestamp (sem parte aleatória para evitar duplicatas)
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            // Pega os últimos 9 dígitos para garantir que caiba em int
            var numero = timestamp.Substring(timestamp.Length - 9);
            return int.Parse(numero);
        }
    }
}
