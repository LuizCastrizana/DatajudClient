namespace DatajudClient.HttpClient
{
    [Serializable]
    public class HttpClientException : Exception
    {
        public string RequestUri { get; set; }

        public string Method { get; set; }

        public string? Content { get; set; }

        public HttpClientException() 
        {
            RequestUri = string.Empty;
            Method = string.Empty;
        }

        public HttpClientException(string message) : base(message) 
        {
            RequestUri = string.Empty;
            Method = string.Empty;
        }

        public HttpClientException(string message, Exception innerException) : base(message, innerException) 
        {
            RequestUri = string.Empty;
            Method = string.Empty;
        }

        public HttpClientException(string message, Exception innerException, string requestUri, string method, string? content = null) : base(message, innerException) 
        {
            RequestUri = requestUri;
            Method = method;
            Content = content;
        }
    }
}
