using AIAssistantMacos.Persistance.Entities;

namespace AIAssistantMacos.Services;

public interface IBlogService
{
    Task<List<Blog>> GetBlogsAsync();
    Task<Blog?> GetByIdAsync(Guid id);
}