using AIAssistantMacos.Persistance;
using AIAssistantMacos.Persistance.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIAssistantMacos.Services;

public class BlogService : IBlogService
{
    private readonly AppDbContext _dbContext;

    public BlogService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Blog>> GetBlogsAsync()
    {
        var list = await _dbContext.Blogs.ToListAsync();
        return list;
    }

    public async Task<Blog?> GetByIdAsync(Guid id)
    {
        var foundBlog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
        
        return foundBlog;
    }
}