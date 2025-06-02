using System.Net;

namespace Cinema.Exceptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ApiException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
