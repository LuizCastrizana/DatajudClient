using DatajudClient.Importador.DataAccess;
using DatajudClient.Importador.Services;

namespace DatajudClient.Importador
{
    class Program
    {
        static void Main(string[] args)
        {
             Importacao();
        }

        private static void Importacao()
        {
            var caminhoImportar = "..\\..\\..\\..\\DatajudClient.Importador\\Importar\\";

            while (true)
            {
                try
                {
                    var comando = string.Empty;
                    var nomeArquivo = string.Empty;
                    var contador = 0;

                    Console.Clear();
                    Console.Write("Digite 1 para estados ou 2 para processos: ");
                    comando = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(comando))
                        throw new Exception("Comando inválido.");

                    if (comando.ToUpper() == "SAIR")
                        break;

                    Console.Write("Insira nome do arquivo: ");
                    nomeArquivo = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nomeArquivo))
                        throw new Exception("Nome do arquivo inválido.");

                    switch (comando)
                    {
                        case "1":
                            nomeArquivo = string.Concat(nomeArquivo, ".csv");

                            var estados = LeitorEstados.LerEstados(caminhoImportar + nomeArquivo);

                            using (var db = new DataBase())
                            {
                                foreach (var estado in estados)
                                {
                                    contador += db.Insert(estado);
                                }
                            }

                            Console.Clear();
                            Console.WriteLine(string.Concat("Qtd. de estados incluídos: ", contador));

                            break;
                        case "2":
                            nomeArquivo = string.Concat(nomeArquivo, ".xlsx");

                            var processos = LeitorProcessos.LerPlanilha(caminhoImportar + nomeArquivo);

                            using (var db = new DataBase())
                            {
                                foreach (var processo in processos)
                                {
                                    contador += db.Insert(processo);
                                }
                            }

                            Console.Clear();
                            Console.WriteLine(string.Concat("Qtd. de processos incluídos: ", contador));

                            break;
                        default:
                            throw new Exception("Comando inválido.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadKey();
            }
        }
    }
}