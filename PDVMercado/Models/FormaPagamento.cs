namespace SistemaMercado.Models
{
    public static class FormaPagamento
    {
        public const string Dinheiro = "Dinheiro";
        public const string CartaoCredito = "Cartão Crédito";
        public const string CartaoDebito = "Cartão Débito";
        public const string Pix = "PIX";
        public const string ValeAlimentacao = "Vale Alimentação";

        public static List<string> ObterTodas()
        {
            return new List<string>
            {
                Dinheiro,
                CartaoCredito,
                CartaoDebito,
                Pix,
                ValeAlimentacao
            };
        }

        public static bool RequerTroco(string formaPagamento)
        {
            return formaPagamento == Dinheiro;
        }
    }
}
