using System.Diagnostics.CodeAnalysis;

namespace TestTask.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class BaseMock
{
    public static bool ShouldThrowException { get; set; }
    public static bool ShouldRpcThrowException { get; set; }
    public static string ExceptionMessage => "Test exception";

    protected BaseMock() { }

    protected static void ThrowExceptionIfNeeded()
    {
        if (ShouldThrowException)
        {
            throw new Exception(ExceptionMessage);
        }
    }
}