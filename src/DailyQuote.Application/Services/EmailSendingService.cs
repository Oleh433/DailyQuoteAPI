using DailyQuote.Application.DTO;
using DailyQuote.Application.ServiceContracts;
using System.Net;
using System.Net.Mail;

namespace DailyQuote.Application.Services
{
    public class EmailSendingService : IEmailSendingService
    {
        public async Task SendQuoteAsync(QuoteResponse quote, string email)
        {
            string mail = "ogstudying@gmail.com";
            string password = "iocvfsyfyregcsjl";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };

            string body = """
                <!DOCTYPE html>
                <html>
                <head>
                  <meta charset="UTF-8">
                  <title>Daily Quote</title>
                  <style>
                    body {
                      font-family: Arial, sans-serif;
                      background-color: #f4f4f4;
                      padding: 20px;
                    }

                    .email-container {
                      max-width: 600px;
                      margin: auto;
                      background-color: #ffffff;
                      border-radius: 8px;
                      box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                      overflow: hidden;
                    }

                    .header {
                      background-color: #4caf50;
                      color: white;
                      text-align: center;
                      padding: 20px;
                    }

                    .content {
                      padding: 30px 20px;
                      text-align: center;
                    }

                    .quote {
                      font-size: 20px;
                      font-style: italic;
                      margin: 20px 0;
                      color: #333;
                    }

                    .author {
                      font-size: 16px;
                      font-weight: bold;
                      color: #555;
                    }

                    .footer {
                      font-size: 12px;
                      color: #888;
                      text-align: center;
                      padding: 10px;
                    }
                  </style>
                </head>
                <body>
                  <div class="email-container">
                    <div class="header">
                      <h2>🌟 Your Daily Quote</h2>
                    </div>
                    <div class="content">
                      <p class="quote">"You can do it"</p>
                    </div>
                    <div class="footer">
                      You received this email because you subscribed to DailyQuote.<br>
                      © 2025 DailyQuote
                    </div>
                  </div>
                </body>
                </html>
                """;

            var mailMessage = new MailMessage(from: mail, to: email
                , subject: "Daily Quote", body: body);

            mailMessage.IsBodyHtml = true;

            await smtpClient.SendMailAsync( mailMessage);
        }
    }
}
