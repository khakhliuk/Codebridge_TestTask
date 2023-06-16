using Codebridge_TestTask.Entity;
using Codebridge_TestTask.Interfaces;
using Codebridge_TestTask.Models;
using Codebridge_TestTask.Services;
using Moq;
using NUnit.Framework;

namespace TestTask.Tests.Services;

public class DogServiceTests : BaseUnitTest
{
    private IDogService _dogService;
    private Dog _testDog1;
    private Dog _testDog2;

    [SetUp]
    public void SetUp()
    {
        InitializeMocks();

        _dogService = new DogService(Context, Mapper);

        _testDog1 = new Dog()
        {
            Id = 1,
            Name = "Mark",
            Color = "black & white",
            TailLenght = 8,
            Weight = 54
        };
        
        _testDog2 = new Dog()
        {
            Id = 2,
            Name = "Lilu",
            Color = "white & black",
            TailLenght = 14,
            Weight = 13
        };

        Context.Dogs.Add(_testDog1);
        Context.Dogs.Add(_testDog2);
        Context.SaveChanges();
    }
    
    
    [Test]
    public void GetAsync_should_Return_Dogs()
    {
        //Arrange
        ICollection<DogResponse> expected = new List<DogResponse>();
        expected.Add(Mapper.Map<DogResponse>(_testDog1));
        expected.Add(Mapper.Map<DogResponse>(_testDog2));
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(new SearchQuery()).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        Assert.That(actual.Result, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetAsync_should_Return_Dogs_Page1_Size1()
    {
        //Arrange
        ICollection<DogResponse> expected = new List<DogResponse>();
        expected.Add(Mapper.Map<DogResponse>(_testDog1));
        
        SearchQuery testQuery = new SearchQuery()
        {
            PageNumber = 1,
            Size = 1
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        Assert.That(actual.Result, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetAsync_should_Return_Dogs_Page1_Size2()
    {
        //Arrange
        ICollection<DogResponse> expected = new List<DogResponse>();
        expected.Add(Mapper.Map<DogResponse>(_testDog1));
        expected.Add(Mapper.Map<DogResponse>(_testDog2));
        
        SearchQuery testQuery = new SearchQuery()
        {
            PageNumber = 1,
            Size = 2
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        Assert.That(actual.Result, Is.EqualTo(expected));
    }

    [Test]
    public void GetAsync_should_Return_Paging_Error_BadRequest()
    {
        //Arrange
        SearchQuery testQuery = new SearchQuery()
        {
            PageNumber = 2,
            Size = 2
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(Error.BadRequest()));
    }

    
    [Test]
    public void GetAsync_should_Return_Dogs_OrderedByName()
    {
        //Arrange
        ICollection<DogResponse> expected = new List<DogResponse>();
        expected.Add(Mapper.Map<DogResponse>(_testDog2));
        expected.Add(Mapper.Map<DogResponse>(_testDog1));
        
        SearchQuery testQuery = new SearchQuery()
        {
            Attribute = "Name"
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        Assert.That(actual.Result, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetAsync_should_Return_Dogs_OrderedByName_InDescending()
    {
        //Arrange
        ICollection<DogResponse> expected = new List<DogResponse>();
        expected.Add(Mapper.Map<DogResponse>(_testDog1));
        expected.Add(Mapper.Map<DogResponse>(_testDog2));
        
        SearchQuery testQuery = new SearchQuery()
        {
            Attribute = "Name",
            Order = "desc"
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        Assert.That(actual.Result, Is.EqualTo(expected));
    }
    
    [Test]
    public void GetAsync_should_Return_Attribute_Error_BadRequest()
    {
        //Arrange
        SearchQuery testQuery = new SearchQuery()
        {
            Attribute = "Something"
        };
        
        //Act
        ResponseWrapper<IEnumerable<DogResponse>> actual = _dogService.GetAsync(testQuery).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(Error.BadRequest()));
    }
    
    [Test]
    public void AddAsync_should_add_NewDog()
    {
        //Arrange
        CreateDogRequest testDog = new CreateDogRequest()
        {
            Name = "NewDog",
            Color = "red",
            TailLenght = 2,
            Weight = 22
        };
        
        Dog expectedDog = new Dog()
        {
            Id = 3,
            Name = "NewDog",
            Color = "red",
            TailLenght = 2,
            Weight = 22
        };
        
        //Act
        ResponseWrapper<string> actual = _dogService.AddAsync(testDog, CancellationToken.None).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(null));
        ContextMock.Verify(o => o.SaveChangesAsync(CancellationToken.None), Times.Once);
        CollectionAssert.Contains(Context.Dogs, expectedDog);
    }
    
    [Test]
    public void AddAsync_should_return_Error_BadRequest()
    {
        //Arrange
        CreateDogRequest testDog = new CreateDogRequest()
        {
            Name = "Mark",
            Color = "red",
            TailLenght = 2,
            Weight = 22
        };
        
        //Act
        ResponseWrapper<string> actual = _dogService.AddAsync(testDog, CancellationToken.None).GetAwaiter().GetResult();
        
        //Assert
        Assert.That(actual.Error, Is.EqualTo(Error.BadRequest()));
    }
}