using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AIAssistantMacos.Pages;

public class CheckoutSuccessModel : PageModel
{
    public string ActivationCode { get; set; }
    
    public void OnGet()
    {
        ActivationCode = Request.Query["code"];
    }
}
