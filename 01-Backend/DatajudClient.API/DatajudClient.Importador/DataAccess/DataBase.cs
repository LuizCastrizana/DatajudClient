using DatajudClient.Importador.Models;
using MySqlConnector;

namespace DatajudClient.Importador.DataAccess
{
    public class DataBase : IDisposable
    {
        private readonly string _connectionString = "server=localhost;database=datajudclientdb;user=root;password=mysqlroot";
        private MySqlConnection Connection { get; set; }

        public DataBase()
        {
            Connection = new MySqlConnection(_connectionString);
            Connection.Open();
        }

        public int Insert(Estado estado)
        {
            if(EstadoExiste(estado.Uf))
                return 0;

            var sql = "INSERT INTO estado (nome, uf, codigoibge, datainclusao, dataalteracao, ativo) VALUES (@nome, @uf, @codigoibge, @datainclusao, @dataalteracao, @ativo)";

            using (var command = new MySqlCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("@nome", estado.Nome);
                command.Parameters.AddWithValue("@uf", estado.Uf);
                command.Parameters.AddWithValue("@codigoibge", estado.IBGE);
                command.Parameters.AddWithValue("@datainclusao", DateTime.Now);
                command.Parameters.AddWithValue("@dataalteracao", DateTime.MinValue);
                command.Parameters.AddWithValue("@ativo", 1);

                var retorno = command.ExecuteNonQuery();

                return retorno;
            }
        }

        public int Insert(ProcessoPlanilha processo)
        {
            if(ProcessoExiste(processo.NumeroDoProcesso))
                return 0;

            var sql = "INSERT INTO processo (numeroprocesso, nomecaso, vara, comarca, observacao, estadoid, tribunalid, datainclusao, dataalteracao, ativo) " +
                "VALUES (@numeroprocesso, @nomecaso, @vara, @comarca, @observacao, @estadoid, @tribunalid, @datainclusao, @dataalteracao, @ativo)";

            try
            {
                var dadosTribunal = ObterDadosTribunalPorNumero(processo.NumeroDoProcesso.Substring(14, 2));

                using (var command = new MySqlCommand(sql, Connection))
                {
                    command.Parameters.AddWithValue("@numeroprocesso", processo.NumeroDoProcesso);
                    command.Parameters.AddWithValue("@nomecaso", processo.NomeDoCaso);
                    command.Parameters.AddWithValue("@vara", processo.Juizo);
                    command.Parameters.AddWithValue("@comarca", processo.Comarca);
                    command.Parameters.AddWithValue("@observacao", processo.TipoDeAcao);
                    command.Parameters.AddWithValue("@estadoid", dadosTribunal.estadoId);
                    command.Parameters.AddWithValue("@tribunalid", dadosTribunal.tribunalId);
                    command.Parameters.AddWithValue("@datainclusao", DateTime.Now);
                    command.Parameters.AddWithValue("@dataalteracao", DateTime.MinValue);
                    command.Parameters.AddWithValue("@ativo", 1);

                    var retorno = command.ExecuteNonQuery();

                    return retorno;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }

        private bool EstadoExiste(string uf)
        {
            var sql = "SELECT COUNT(*) FROM estado WHERE uf = @uf";
            using (var command = new MySqlCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("@uf", uf);
                var retorno = Convert.ToInt32(command.ExecuteScalar());
                return retorno > 0;
            }
        }

        private bool ProcessoExiste(string numero)
        {
            var sql = "SELECT COUNT(*) FROM processo WHERE numeroprocesso = @numeroprocesso";
            using (var command = new MySqlCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("@numeroprocesso", numero);
                var retorno = Convert.ToInt32(command.ExecuteScalar());
                return retorno > 0;
            }
        }

        private (int tribunalId, int estadoId) ObterDadosTribunalPorNumero(string numero)
        {
            var retorno = (tribunalId: 0, estadoId: 0);
            var sql = "SELECT id, estadoid FROM tribunal WHERE numero = @numero";
            using (var command = new MySqlCommand(sql, Connection))
            {
                command.Parameters.AddWithValue("@numero", numero);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retorno.tribunalId = reader.GetInt32("id");
                        retorno.estadoId = reader.GetInt32("estadoid");
                    }
                }
            }
            return retorno;
        }
    }
}
