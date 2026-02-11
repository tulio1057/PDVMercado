namespace PDVMercado.Models
{
    public enum FormaPagamento
    {
        Dinheiro,
        CartaoCredito,
        CartaoDebito,
        Pix,  // Era: PIX - agora é Pix
        Boleto,
        Cheque,
        Outros
    }

    public static class FormaPagamentoExtensions
    {
        public static string ObterDescricao(this FormaPagamento forma)
        {
            return forma switch
            {
                FormaPagamento.Dinheiro => "Dinheiro",
                FormaPagamento.CartaoCredito => "Cartão de Crédito",
                FormaPagamento.CartaoDebito => "Cartão de Débito",
                FormaPagamento.Pix => "PIX",
                FormaPagamento.Boleto => "Boleto",
                FormaPagamento.Cheque => "Cheque",
                FormaPagamento.Outros => "Outros",
                _ => "Não especificado"
            };
        }

        public static bool PrecisaTroco(this FormaPagamento forma)
        {
            return forma == FormaPagamento.Dinheiro;
        }
    }
}
