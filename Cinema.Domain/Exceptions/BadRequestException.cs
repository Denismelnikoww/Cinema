using System.Net;

namespace Cinema.Exceptions
{
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message)
            : base(HttpStatusCode.BadRequest, message) {}
    }
}
