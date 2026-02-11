using PDVMercado.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PDVMercado.Utils
{
    public static class GeradorNotaFiscal
    {
        public static void Gerar(Venda venda, string? caminhoArquivo = null) // ✅ CORRIGIDO: Parâmetro anulável
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .AlignCenter()
                        .Text("NOTA FISCAL")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            // Cabeçalho da nota
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().BorderBottom(1).PaddingBottom(5).Text($"Número: {venda.NumeroNota}");
                                table.Cell().BorderBottom(1).PaddingBottom(5).AlignRight().Text($"Data: {venda.DataVenda:dd/MM/yyyy HH:mm}");

                                table.Cell().BorderBottom(1).PaddingVertical(5).Text($"Cliente: CONSUMIDOR FINAL");
                                table.Cell().BorderBottom(1).PaddingVertical(5).AlignRight().Text($"Operador: {venda.UsuarioNome}");
                            });

                            // Itens da venda
                            column.Item().PaddingTop(10).Table(itemTable =>
                            {
                                itemTable.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(3); // Produto
                                    columns.ConstantColumn(60); // Qtd
                                    columns.RelativeColumn(2); // Unitário
                                    columns.RelativeColumn(2); // Total
                                });

                                itemTable.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Produto");
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text("Qtd");
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignRight().Text("Unitário");
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).AlignRight().Text("Total");
                                });

                                foreach (var item in venda.Itens)
                                {
                                    itemTable.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.ProdutoNome);
                                    itemTable.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text(item.Quantidade.ToString());
                                    itemTable.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(item.PrecoUnitario.ToString("C2"));
                                    itemTable.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(item.Total.ToString("C2"));
                                }
                            });

                            // Totais
                            column.Item().PaddingTop(20).AlignRight().Table(totalTable =>
                            {
                                totalTable.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.ConstantColumn(150);
                                });

                                totalTable.Cell().Text("Subtotal:").SemiBold();
                                totalTable.Cell().AlignRight().Text(venda.ValorTotal.ToString("C2"));

                                totalTable.Cell().Text("Forma de Pagamento:").SemiBold();
                                totalTable.Cell().AlignRight().Text(venda.FormaPagamento.ToString()); // ✅ CORRIGIDO: ToString() no enum

                                // ✅ CORRIGIDO: Comparação com enum ao invés de string
                                if (venda.FormaPagamento == FormaPagamento.Dinheiro)
                                {
                                    totalTable.Cell().Text("Valor Pago:").SemiBold();
                                    totalTable.Cell().AlignRight().Text(venda.ValorPago.ToString("C2"));

                                    totalTable.Cell().Text("Troco:").SemiBold();
                                    totalTable.Cell().AlignRight().Text(venda.Troco.ToString("C2"));
                                }

                                totalTable.Cell().BorderTop(1).PaddingTop(5).Text("TOTAL:").Bold().FontSize(12);
                                totalTable.Cell().BorderTop(1).PaddingTop(5).AlignRight().Text(venda.ValorTotal.ToString("C2")).Bold().FontSize(12);
                            });

                            // Rodapé
                            column.Item().PaddingTop(30).AlignCenter().Text(text =>
                            {
                                text.Span("Obrigado pela preferência!").Bold();
                                text.EmptyLine();
                                text.Span("Volte sempre!");
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("PDV Mercado - ");
                            text.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        });
                });
            });

            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                caminhoArquivo = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"NotaFiscal_{venda.NumeroNota}.pdf"
                );
            }

            documento.GeneratePdf(caminhoArquivo);
        }
    }
}
