using AutoMapper;
using Codebridge_TestTask.Entity;
using Codebridge_TestTask.Models;

namespace Codebridge_TestTask.MapperConfigs;

public class DogMappingProfile : Profile
{
    public DogMappingProfile()
    {
        CreateMap<Dog, CreateDogRequest>().ReverseMap();
        CreateMap<Dog, UpdateDogRequest>().ReverseMap();
        CreateMap<Dog, DogResponse>().ReverseMap();
    }
}