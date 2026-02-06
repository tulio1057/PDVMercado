using Google.Cloud.Firestore;

namespace SistemaMercado.Models
{
    [FirestoreData]
    public class Usuario
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Nome { get; set; }

        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string SenhaHash { get; set; }

        [FirestoreProperty]
        public string NivelAcesso { get; set; } // "Caixa", "Gerente", "Admin"

        [FirestoreProperty]
        public bool Ativo { get; set; }

        [FirestoreProperty]
        public DateTime DataCadastro { get; set; }

        [FirestoreProperty]
        public DateTime? UltimoAcesso { get; set; }

        public bool TemPermissaoCaixa => Ativo && (NivelAcesso == "Caixa" || NivelAcesso == "Gerente" || NivelAcesso == "Admin");
        public bool TemPermissaoGerente => Ativo && (NivelAcesso == "Gerente" || NivelAcesso == "Admin");
        public bool TemPermissaoAdmin => Ativo && NivelAcesso == "Admin";
    }
}