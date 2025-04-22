using AIAssistantMacos.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace AIAssistantMacos.ApiControllers;

[ApiController]
[Route("api/checkout")]
public class CheckoutController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private HttpClient _httpClient;

    private string BaseUrl { get; set; }
    
    private readonly string _priceId;
    
    private class CreateCodeResponse
    {
        public string Code { get; set; }
    }

    public CheckoutController(IHttpContextAccessor httpContext, IConfiguration configuration, IEmailService emailService, HttpClient httpClient)
    {
        
        _httpContext = httpContext;
        _configuration = configuration;
        BaseUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}";
        _priceId = _configuration["Stripe:PriceId"];
        _emailService = emailService;
        _httpClient = httpClient;
    }

    [HttpGet("success")]
    public async Task<IActionResult> CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        var name = session.CustomerDetails?.Name ?? "there";
        var email = session.CustomerDetails?.Email;

        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Missing email in session");

        var response = await _httpClient.PostAsync("/api/code/create-code", null);
        if (!response.IsSuccessStatusCode)
            return StatusCode(500, "Failed to generate activation code");

        var json = await response.Content.ReadFromJsonAsync<CreateCodeResponse>();
        var activationCode = json?.Code ?? "N/A";

        await _emailService.SendEmailAsync(email, name, activationCode);

        return Redirect($"{BaseUrl}/success?code={activationCode}");
    }
    
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckoutOrder()
    {
        var options = new SessionCreateOptions
        {
            SuccessUrl = BaseUrl + "/api/checkout/success?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = $"{BaseUrl}/fail",
            Mode = "subscription",
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = _priceId,
                    Quantity = 1,
                },
            },
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return Redirect(session.Url);
    }
}