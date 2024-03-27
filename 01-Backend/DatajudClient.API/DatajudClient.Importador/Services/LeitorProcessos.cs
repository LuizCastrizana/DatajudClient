using ClosedXML.Excel;
using DatajudClient.Importador.Models;

namespace DatajudClient.Importador.Services
{
    public static class LeitorProcessos
    {
        public static List<ProcessoPlanilha> LerPlanilha(string caminhoArquivo)
        {
            var xls = new XLWorkbook(caminhoArquivo);
            var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
            var totalLinhas = planilha.Rows().Count();
            var totalColunas = planilha.Columns().Count();
            var processos = new List<ProcessoPlanilha>();
            var DictionaryColunas = new Dictionary<int, string>();

            for (int c = 1; c <= totalColunas; c++)
            {
                var itemCabecalho = planilha.Cell(1, c).Value.ToString().ToUpper();
                DictionaryColunas.Add(c, itemCabecalho);
            }

            for (int l = 2; l <= totalLinhas; l++)
            {
                var processo = new ProcessoPlanilha();

                for (int c = 1; c <= totalColunas; c++)
                {
                    var valor = planilha.Cell(l, c).Value.ToString();
                    var coluna = DictionaryColunas[c];

                    switch (coluna)
                    {
                        case "CÓDIGO":
                            processo.Codigo = valor;
                            break;
                        case "SITUAÇÃO":
                            processo.Situacao = valor;
                            break;
                        case "CATEGORIA":
                            processo.Categoria = valor;
                            break;
                        case "TIPO DE AÇÃO":
                            processo.TipoDeAcao = valor;
                            break;
                        case "CLIENTE":
                            processo.Cliente = valor;
                            break;
                        case "CONDIÇÃO DO CLIENTE":
                            processo.CondicaoDoCliente = valor;
                            break;
                        case "ADVERSO":
                            processo.Adverso = valor;
                            break;
                        case "CONDIÇÃO DO ADVERSO":
                            processo.CondicaoDoAdverso = valor;
                            break;
                        case "NÚMERO DO PROCESSO":
                            processo.NumeroDoProcesso = ApenasNumeros(valor);
                            break;
                        case "DATA DISTRIBUIÇÃO":
                            DateTime.TryParse(valor, out DateTime distribuicao);
                            processo.DataDistribuicao = distribuicao;
                            break;
                        case "JUÍZO":
                            processo.Juizo = valor;
                            break;
                        case "COMARCA":
                            processo.Comarca = valor;
                            break;
                        case "DATA DO ANDAMENTO":
                            DateTime.TryParse(valor, out DateTime andamento);
                            processo.DataDoAndamento = andamento;
                            break;
                        case "ANDAMENTO":
                            processo.Andamento = valor;
                            break;
                        case "OBSERVAÇÕES DO ANDAMENTO":
                            processo.ObservacoesDoAndamento = valor;
                            break;
                        case "CADASTRADO EM":
                            DateTime.TryParse(valor, out DateTime cadastradoEm);
                            processo.CadastradoEm = cadastradoEm;
                            break;
                        case "NOME DO CASO":
                            processo.NomeDoCaso = valor;
                            break;

                    }
                }

                if (string.IsNullOrWhiteSpace(processo.NomeDoCaso))
                    processo.NomeDoCaso = processo.TipoDeAcao + " - " + processo.Adverso;

                processos.Add(processo);
            }

            return processos;
        }

        private static string ApenasNumeros(string texto)
        {
            return new string(texto.Where(char.IsDigit).ToArray());
        }
    }
}
