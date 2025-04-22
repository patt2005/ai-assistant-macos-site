using SendGrid;
using SendGrid.Helpers.Mail;

namespace AIAssistantMacos.Services;

public class EmailService : IEmailService
{
    private readonly string _apiKey;

    public EmailService()
    {
        _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
    }
    
    public async Task SendEmailAsync(string toEmail, string name, string activationCode)
    {
        var client = new SendGridClient(_apiKey);

        var from = new EmailAddress("petru@codbun.com", "Agent AI");
        var to = new EmailAddress(toEmail, name);

        var subject = "Your Agent AI Activation Code";

        var plainTextContent = $"Hi {name},\n\nThanks for your purchase!\nYour activation code is:\n{activationCode}";

        var htmlContent = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: 'Inter', sans-serif;
                    background-color: #f8f9fb;
                    color: #333;
                    padding: 40px 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: auto;
                    background-color: #ffffff;
                    padding: 32px;
                    border-radius: 12px;
                    box-shadow: 0 0 20px rgba(0,0,0,0.05);
                }}
                .header {{
                    font-size: 24px;
                    font-weight: 700;
                    color: #3f00bf;
                    margin-bottom: 16px;
                }}
                .code {{
                    font-size: 24px;
                    font-weight: 600;
                    background-color: #f2f2f2;
                    padding: 12px 24px;
                    border-radius: 8px;
                    display: inline-block;
                    margin: 20px 0;
                    letter-spacing: 2px;
                    color: #3f00bf;
                }}
                .footer {{
                    margin-top: 40px;
                    font-size: 12px;
                    color: #999;
                    text-align: center;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>Welcome to Agent AI 👋</div>

                <p>Hi {name},</p>
                <p>Thanks for upgrading to <strong>Agent AI Pro</strong>! Below is your activation code:</p>

                <div class='code'>{activationCode}</div>

                <p>Paste this code in the app to unlock all Pro features.</p>

                <p>Enjoy your journey with your new assistant!</p>

                <div class='footer'>
                    &copy; 2025 Agent AI · All rights reserved
                </div>
            </div>
        </body>
        </html>
        ";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        await client.SendEmailAsync(msg);
    }
}