using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Codebridge_TestTask.Data;
using Codebridge_TestTask.Interfaces;
using Codebridge_TestTask.MapperConfigs;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestTask.Tests.Mocks;

namespace TestTask.Tests;

[ExcludeFromCodeCoverage]
public class BaseUnitTest
{
    protected Mock<IAppDbContext> ContextMock { get; set; } = null!;
    protected IAppDbContext Context { get; set; } = null!;
    private AppDbContext _applicationDbContext = null!;
    protected Mapper Mapper { get; set; } = null!;
    
    public void InitializeMocks()
    {
        ApplicationDbContextMock.ShouldThrowException = false;

        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "Test")
            .Options;

        _applicationDbContext = new AppDbContext(options);
        _applicationDbContext.Database.EnsureDeleted();
        ContextMock = ApplicationDbContextMock.SetupMock(_applicationDbContext);
        Context = ContextMock.Object;

        MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DogMappingProfile());
        });
        Mapper = new(mapperConfiguration);

    }
}