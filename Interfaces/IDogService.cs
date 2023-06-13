using Codebridge_TestTask.Entity;
using Codebridge_TestTask.Models;

namespace Codebridge_TestTask.Interfaces;

public interface IDogService
{
    public Task<IEnumerable<DogResponse>> GetAllAsync();
    public Task<string> AddAsync(CreateDogRequest createDog, CancellationToken cancellationToken);
    public Task<string> UpdateAsync(UpdateDogRequest updateDog, CancellationToken cancellationToken);
    public Task<string> DeleteAsync(long id, CancellationToken cancellationToken);
}