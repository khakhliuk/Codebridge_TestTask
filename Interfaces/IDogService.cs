using Codebridge_TestTask.Entity;
using Codebridge_TestTask.Models;

namespace Codebridge_TestTask.Interfaces;

public interface IDogService
{
    public Task<ResponseWrapper<IEnumerable<DogResponse>>> GetAsync(SearchQuery searchQuery);
    public Task<ResponseWrapper<string>> AddAsync(CreateDogRequest createDog, CancellationToken cancellationToken);
    public Task<ResponseWrapper<string>> UpdateAsync(UpdateDogRequest updateDog, CancellationToken cancellationToken);
    public Task<ResponseWrapper<string>> DeleteAsync(long id, CancellationToken cancellationToken);
}