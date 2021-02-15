using System;
using System.Net;

namespace SimpleNotesServer.Sdk.Exceptions
{
    public class InvalidRequestException : Exception
    {
        public readonly HttpStatusCode StatusCode;

        public InvalidRequestException(string message,
            HttpStatusCode statusCode,
            Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
