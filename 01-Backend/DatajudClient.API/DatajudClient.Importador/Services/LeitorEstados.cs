using System.Text;
using DatajudClient.Importador.Models;

namespace DatajudClient.Importador.Services
{
    public static class LeitorEstados
    {
        public static List<Estado> LerEstados(string caminhoArquivo)
        {
            var streamReader = new StreamReader(caminhoArquivo, Encoding.Latin1, true);
            var contaLinha = 0;
            var linha = string.Empty;
            var estados = new List<Estado>();
            string[] linhaseparada = { };

            if (streamReader != null)
            {
                while (true)
                {
                    contaLinha++;

                    linha = streamReader.ReadLine();

                    if (linha == null)
                        break;
                    if (contaLinha == 1)
                        continue;

                    linhaseparada = linha.Split(';');

                    estados.Add(new Estado
                    {
                        IBGE = linhaseparada[0],
                        Nome = linhaseparada[1],
                        Uf = linhaseparada[2],
                        Regiao = linhaseparada[3],
                        QtdMun = linhaseparada[4],
                        Sintaxe = linhaseparada[5]
                    });
                }
            }

            return estados;
        }
    }
}
