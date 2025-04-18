using AIAssistantMacos.Persistance.Entities;
using AIAssistantMacos.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AIAssistantMacos.Pages;

public class BlogListModel : PageModel
{
    private readonly ILogger<BlogListModel> _logger;
    private readonly IBlogService _blogService;
    
    public List<Blog> Posts { get; set; }
    
    public BlogListModel(ILogger<BlogListModel> logger, IBlogService blogService)
    {
        _logger = logger;
        _blogService = blogService;
    }

    public async Task OnGet()
    {
        Posts = await _blogService.GetBlogsAsync();
    }
}