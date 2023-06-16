namespace Codebridge_TestTask.Models;

public class ResponseWrapper<T>
{
    public T Result { get; set; }
    public Error Error { get; set; }


    public ResponseWrapper(T result)
    {
        Result = result;
    }

    public ResponseWrapper(Error error)
    {
        Error = error;
    }

    public ResponseWrapper()
    {
    }

    public static ResponseWrapper<string> Success()
    {
        return new ResponseWrapper<string>();
    }

    public static ResponseWrapper<T> Success(T body)
    {
        return new ResponseWrapper<T>(body);
    }

    public static ResponseWrapper<T> Failure(Error error)
    {
        return new ResponseWrapper<T>(error);
    }
}