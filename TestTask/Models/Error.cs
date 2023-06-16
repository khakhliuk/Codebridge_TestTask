using System.Net;

namespace Codebridge_TestTask.Models;

public class Error
{
    public string Message { get; set; } = null!;
    public HttpStatusCode HttpStatusCode { get; set; }

    public static Error Of(string message, HttpStatusCode status)
    {
        return new Error()
        {
            Message = message,
            HttpStatusCode = status,
        };
    }

    public static Error AlreadyExist()
    {
        return new Error()
        {
            Message = "Already exist",
            HttpStatusCode = HttpStatusCode.UnprocessableEntity,
        };
    }
    
    public static Error BadRequest()
    {
        return new Error()
        {
            Message = "Bad Request",
            HttpStatusCode = HttpStatusCode.BadRequest,
        };
    }

    public override int GetHashCode()
    {
        HashCode hashCode = new ();
        hashCode.Add(Message);
        hashCode.Add(HttpStatusCode);
        return hashCode.ToHashCode();
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Error other &&
               Message == other.Message &&
               HttpStatusCode == other.HttpStatusCode;
    }
}