namespace PDVMercado.Models
{
    public enum TipoUsuario
    {
        Administrador,
        Vendedor,
        Gerente
    }

    public class Usuario
    {
        public string Id { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TipoUsuario Tipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        
        // Alias para compatibilidade
        public string SenhaHash => Senha;
        
        // Métodos de permissão
        public bool TemPermissaoAdmin() => Tipo == TipoUsuario.Administrador;
        public bool TemPermissaoGerente() => Tipo == TipoUsuario.Gerente || Tipo == TipoUsuario.Administrador;
        public bool TemPermissaoCaixa() => Ativo;

        public Usuario()
        {
            Id = Guid.NewGuid().ToString();
            DataCadastro = DateTime.Now;
            Ativo = true;
        }
    }
}
