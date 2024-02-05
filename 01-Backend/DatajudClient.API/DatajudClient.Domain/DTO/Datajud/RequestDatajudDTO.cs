namespace DatajudClient.Domain.DTO.Datajud
{
    public class RequestDatajudDTO
    {
        public Query query { get; set; }
    }

    public class Query
    {
        public Match match { get; set; }
    }

    public class Match
    {
        public string numeroProcesso { get; set; }
    }
}

