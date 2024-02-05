using System.Net;

namespace DatajudClient.HttpClient
{
    public class RetornoHttpClient
    {
        public RetornoHttpClient()
        {
            Content = string.Empty;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }
    }
}
