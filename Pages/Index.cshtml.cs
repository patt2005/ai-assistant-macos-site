using AIAssistantMacos.Persistance.Entities;
using AIAssistantMacos.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AIAssistantMacos.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBlogService _blogService;
    
    public List<Blog> Blogs { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    public async Task OnGet()
    {
        Console.WriteLine("getting blogs");
        Blogs = await _blogService.GetBlogsAsync();
        Console.WriteLine($"Found {Blogs.Count} blogs");
    }
}