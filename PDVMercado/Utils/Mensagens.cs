using System.Windows.Forms;

namespace PDVMercado.Utils
{
    public static class Mensagens
    {
        public static void Info(string mensagem, string titulo = "Informação")
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Sucesso(string mensagem, string titulo = "Sucesso")
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Aviso(string mensagem, string titulo = "Atenção")
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Erro(string mensagem, string titulo = "Erro")
        {
            MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string mensagem, string titulo = "Confirmação")
        {
            DialogResult resultado = MessageBox.Show(
                mensagem,
                titulo,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            return resultado == DialogResult.Yes;
        }

        public static DialogResult Pergunta(string mensagem, string titulo = "Pergunta")
        {
            return MessageBox.Show(
                mensagem,
                titulo,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );
        }
    }
}
