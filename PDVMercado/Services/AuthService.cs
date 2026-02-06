using SistemaMercado.Data;
using SistemaMercado.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SistemaMercado.Services
{
    public class AuthService
    {
        private readonly FirestoreDb _firestoreDb;
        private Usuario _usuarioLogado;

        public Usuario UsuarioLogado => _usuarioLogado;
        public bool EstaLogado => _usuarioLogado != null;

        public AuthService()
        {
            _firestoreDb = FirebaseContext.Instance.FirestoreDb;
        }

        public async Task<Usuario> LoginAsync(string email, string senha)
        {
            try
            {
                var collection = _firestoreDb.Collection("usuarios");

                var query = collection
                    .WhereEqualTo("Email", email)
                    .WhereEqualTo("Ativo", true);

                var snapshot = await query.GetSnapshotAsync();

                if (snapshot.Count == 0)
                    throw new Exception("Usuário não encontrado ou inativo");

                var doc = snapshot.Documents[0];
                var usuario = doc.ConvertTo<Usuario>();
                usuario.Id = doc.Id;

                // ✅ VERIFICAÇÃO CORRETA DE SENHA (BCrypt)
                if (!BCryptNet.Verify(senha, usuario.SenhaHash))
                    throw new Exception("Senha incorreta");

                // Atualizar último acesso
                var updateData = new Dictionary<string, object>
                {
                    { "UltimoAcesso", Timestamp.GetCurrentTimestamp() }
                };

                await doc.Reference.UpdateAsync(updateData);

                _usuarioLogado = usuario;
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro no login: {ex.Message}");
            }
        }

        public void Logout()
        {
            _usuarioLogado = null;
        }

        public async Task<bool> AlterarSenhaAsync(string usuarioId, string novaSenha)
        {
            try
            {
                var docRef = _firestoreDb
                    .Collection("usuarios")
                    .Document(usuarioId);

                // ✅ HASH CORRETO
                var hash = BCryptNet.HashPassword(novaSenha);

                var updateData = new Dictionary<string, object>
                {
                    { "SenhaHash", hash }
                };

                await docRef.UpdateAsync(updateData);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerificarPermissao(string nivelRequerido)
        {
            if (_usuarioLogado == null || !_usuarioLogado.Ativo)
                return false;

            return nivelRequerido switch
            {
                "Admin" => _usuarioLogado.TemPermissaoAdmin,
                "Gerente" => _usuarioLogado.TemPermissaoGerente,
                "Caixa" => _usuarioLogado.TemPermissaoCaixa,
                _ => false
            };
        }
    }
}
