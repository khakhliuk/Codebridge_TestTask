using System.Net;
using AutoMapper;
using Codebridge_TestTask.Data;
using Codebridge_TestTask.Entity;
using Codebridge_TestTask.Interfaces;
using Codebridge_TestTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Codebridge_TestTask.Services;

public class DogService : IDogService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public DogService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ResponseWrapper<IEnumerable<DogResponse>>> GetAllAsync(SearchQuery? searchQuery)
    {
        if (searchQuery.Attribute == null)
        {
            IEnumerable<DogResponse> dogs = _context.Dogs.Select(dog => _mapper.Map<DogResponse>(dog));
        
            return await Task.FromResult(ResponseWrapper<IEnumerable<DogResponse>>.Success(dogs));
        }
        
        return ResponseWrapper<IEnumerable<DogResponse>>.Failure(Error.AlreadyExist(""));
    }

    public async Task<ResponseWrapper<string>> AddAsync(CreateDogRequest createDog, CancellationToken cancellationToken)
    {
        Dog newDog = _mapper.Map<Dog>(createDog);
        Dog? checkDog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Name == newDog.Name, cancellationToken);
        
        if (checkDog != null || newDog.TailLenght < 0 || newDog.Weight < 0)
        {
            return await Task.FromResult(ResponseWrapper<string>.Failure(Error.BadCredentials()));
        }
        
        await _context.Dogs.AddAsync(newDog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(ResponseWrapper<string>.Success("Created."));
    }

    public async Task<ResponseWrapper<string>> UpdateAsync(UpdateDogRequest updateDog, CancellationToken cancellationToken)
    {
        Dog modifyDog = _mapper.Map<Dog>(updateDog);
        _context.Dogs.Attach(modifyDog);
        _context.Entry(modifyDog).Property(r => r.Color).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.Name).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.TailLenght).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.Weight).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(ResponseWrapper<string>.Success("Updated."));
    }

    public async Task<ResponseWrapper<string>> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        Dog? deleteDog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id, cancellationToken);
        _context.Dogs.Remove(deleteDog);
        await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(ResponseWrapper<string>.Success("Deleted."));
    }
}