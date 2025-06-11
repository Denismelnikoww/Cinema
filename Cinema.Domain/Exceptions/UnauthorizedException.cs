using Cinema.Exceptions;
using System.Net;

namespace Cinema.Domain.Exceptions
{
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message)
            : base(HttpStatusCode.Unauthorized, message) { }
    }
}
