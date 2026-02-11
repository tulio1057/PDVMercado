using System;

namespace PDVMercado.Models
{
    public class Configuracao
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NomeEmpresa { get; set; } = "Mercado";
        public string CNPJ { get; set; } = "";
        public string Endereco { get; set; } = "";
        public string Telefone { get; set; } = "";
        public string Email { get; set; } = "";
        public bool ImprimirAutomatico { get; set; } = false;
        public bool AbrirGavetaAutomatico { get; set; } = false;
        public bool PermitirVendaSemEstoque { get; set; } = true;
        public string PortaImpressora { get; set; } = "COM1";
        public string PortaGaveta { get; set; } = "COM1";
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAlteracao { get; set; }
    }
}
