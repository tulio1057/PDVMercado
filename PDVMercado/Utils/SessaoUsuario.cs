using System;
using PDVMercado.Models;

namespace PDVMercado.Utils
{
    public static class SessaoUsuario
    {
        public static Usuario? UsuarioAtual { get; private set; }

        public static void IniciarSessao(Usuario usuario)
        {
            UsuarioAtual = usuario;
        }

        public static void EncerrarSessao()
        {
            UsuarioAtual = null;
        }

        public static bool EstaNaSessao()
        {
            return UsuarioAtual != null;
        }

        public static string ObterNomeUsuario()
        {
            return UsuarioAtual?.Nome ?? "Usuário";
        }

        public static string ObterIdUsuario()
        {
            return UsuarioAtual?.Id ?? "";
        }

        public static bool EhAdministrador()
        {
            return UsuarioAtual?.Tipo == TipoUsuario.Administrador;
        }
    }
}
