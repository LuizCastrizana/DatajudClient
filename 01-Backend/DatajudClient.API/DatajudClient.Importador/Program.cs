using DatajudClient.Importador.DataAccess;
using DatajudClient.Importador.Services;

namespace DatajudClient.Importador
{
    class Program
    {
        static void Main(string[] args)
        {
            var comando = string.Empty;
            var caminhoArquivo = string.Empty;
            var contador = 0;

            while (true)
            {
                try
                {
                    Console.Clear();

                    Console.Write("Digite 1 para estados ou 2 para processos: ");
                    comando = Console.ReadLine();

                    comando = string.IsNullOrWhiteSpace(comando) ? string.Empty : comando;

                    if (comando.ToUpper() == "SAIR")
                        break;

                    if (comando == "1")
                    {
                        contador = 0;

                        Console.Write("Insira caminho do arquivo: ");
                        caminhoArquivo = Console.ReadLine();

                        if (string.IsNullOrEmpty(caminhoArquivo))
                            continue;

                        if (caminhoArquivo.ToUpper() == "SAIR")
                            break;

                        var estados = LeitorEstados.LerEstados(caminhoArquivo);

                        using (var db = new DataBase())
                        {
                            foreach (var estado in estados)
                            {
                                if (db.Insert(estado) == 1)
                                    contador++;
                            }
                        }

                        Console.Clear();
                        Console.WriteLine(string.Concat("Qtd. de estados incluídos: ", contador));
                    }
                    else if (comando == "2")
                    {
                        contador = 0;

                        Console.Write("Insira caminho do arquivo: ");
                        caminhoArquivo = Console.ReadLine();

                        if (string.IsNullOrEmpty(caminhoArquivo))
                            continue;

                        if (caminhoArquivo.ToUpper() == "SAIR")
                            break;

                        var processos = LeitorProcessos.LerPlanilha(caminhoArquivo);

                        using (var db = new DataBase())
                        {
                            foreach (var processo in processos)
                            {
                                if (db.Insert(processo) == 1)
                                    contador++;
                            }
                        }

                        Console.Clear();
                        Console.WriteLine(string.Concat("Qtd. de processos incluídos: ", contador));
                    }
                    else
                    {
                        Console.WriteLine("Comando inválido");
                    }
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            } 
        }
    }
}