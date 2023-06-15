using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Codebridge_TestTask.Data;
using Codebridge_TestTask.Interfaces;
using Moq;

namespace TestTask.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextMock
{
    public static bool ShouldThrowException { get; set; }
    public static string ExceptionMessage => "Test exception";
    
    public static Mock<IAppDbContext> SetupMock(AppDbContext context)
    {
        Mock<IAppDbContext> mock = new();

        mock.Setup(m => m.Dogs).Returns(context.Dogs);

        foreach (PropertyInfo? prop in typeof(IAppDbContext).GetProperties())
        {
            Type type = prop.PropertyType.GenericTypeArguments[0];
            MethodInfo methodInfo = typeof(ApplicationDbContextMock).GetMethod(nameof(SetupContext))!;
            MethodInfo myMethod = methodInfo.MakeGenericMethod(type);
            myMethod.Invoke(null, new object[] { mock, context });
        }

        mock.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns<CancellationToken>(token =>
        {
            if (ShouldThrowException)
            {
                throw new Exception(ExceptionMessage);
            }
            return context.SaveChangesAsync(token);
        });
        mock.Setup(m => m.SaveChanges()).Returns(() =>
        {
            if (ShouldThrowException)
            {
                throw new Exception(ExceptionMessage);
            }
            return context.SaveChanges();
        });

        return mock;
    }
    
    public static void SetupContext<T>(Mock<IAppDbContext> mock, AppDbContext context) where T : class
    {
        mock.Setup(m => m.Entry<T>(It.IsAny<T>())).Returns<T>((entity) => context.Entry(entity));
    }
}