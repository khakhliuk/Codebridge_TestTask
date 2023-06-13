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

    public static Error AlreadyExist(string message)
    {
        return new Error()
        {
            Message = message + "already exist",
            HttpStatusCode = HttpStatusCode.UnprocessableEntity,
        };
    }
    
    public static Error BadCredentials()
    {
        return new Error()
        {
            Message = "Bad Credentials",
            HttpStatusCode = HttpStatusCode.Unauthorized,
        };
    }
}