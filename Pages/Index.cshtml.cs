using AIAssistantMacos.Persistance.Entities;
using AIAssistantMacos.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AIAssistantMacos.Pages;

public class IndexModel : PageModel
{
    private readonly IBlogService _blogService;
    public List<Blog> Blogs { get; set; }

    public IndexModel(IBlogService blogService)
    {
        _blogService = blogService;
    }

    public async Task OnGet()
    {
        Blogs = await _blogService.GetBlogsAsync();
    }
}