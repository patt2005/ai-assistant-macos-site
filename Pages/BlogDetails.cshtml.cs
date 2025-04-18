using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AIAssistantMacos.Persistance.Entities;
using AIAssistantMacos.Services;

public class BlogDetails : PageModel
{
    private readonly IBlogService _blogService;

    public BlogDetails(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [BindProperty(SupportsGet = true)]
    public Guid id { get; set; }

    public Blog Blog { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var blog = await _blogService.GetByIdAsync(id);
        
        if (blog == null)
        {
            return NotFound();
        }

        Blog = blog;

        return Page();
    }
}