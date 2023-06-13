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
    
    public async Task<IEnumerable<DogResponse>> GetAllAsync()
    {
        IEnumerable<DogResponse> dogs = _context.Dogs.Select(dog => _mapper.Map<DogResponse>(dog));
        return await Task.FromResult(dogs);
    }

    public async Task<string> AddAsync(CreateDogRequest createDog, CancellationToken cancellationToken)
    {
        Dog newDog = _mapper.Map<Dog>(createDog);
        await _context.Dogs.AddAsync(newDog, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return await Task.FromResult("Created.");
    }

    public async Task<string> UpdateAsync(UpdateDogRequest updateDog, CancellationToken cancellationToken)
    {
        Dog modifyDog = _mapper.Map<Dog>(updateDog);
        _context.Dogs.Attach(modifyDog);
        
        _context.Entry(modifyDog).Property(r => r.Color).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.Name).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.TailLenght).IsModified = true;
        _context.Entry(modifyDog).Property(r => r.Weight).IsModified = true;
        await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult("Updated.");
    }

    public async Task<string> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        Dog? deleteDog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id, cancellationToken);
        _context.Dogs.Remove(deleteDog);
        await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult("Deleted.");
    }
}