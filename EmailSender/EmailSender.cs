using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailSender
{
    public class EmailSender
    {
        private readonly string sendGridKey;
        private readonly string sendGridSender;
        private readonly string sendGridSenderName;
        private readonly string receiver;

        public EmailSender(IConfiguration configuration)
        {
            sendGridKey = configuration.GetValue<string>("SendGridKey");
            sendGridSender = configuration.GetValue<string>("SendGridSender");
            sendGridSenderName = configuration.GetValue<string>("SendGridSenderName");
            receiver = configuration.GetValue<string>("Receiver");
        }

        [FunctionName("EmailSender")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SendEmail")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EmailData data = JsonConvert.DeserializeObject<EmailData>(requestBody);

            SendGridClient client = new(sendGridKey);
            SendGridMessage message = new();
            message.SetFrom(new EmailAddress(sendGridSender, $"{sendGridSenderName} {data.Sender}"));

            EmailAddress recipients = new(receiver, data.Sender);

            message.AddTo(recipients);
            message.SetSubject(data.Subject);
            message.AddContent(MimeType.Text, data.Message);

            log.LogInformation("[INFO] Sending mail...");

            Response result = await client.SendEmailAsync(message);

            if (result.IsSuccessStatusCode)
            {
                log.LogInformation("[INFO] Email was sent successfully...");
                return new OkObjectResult("Email inviata correttamente");
            }

            log.LogInformation("[ERROR] There's beeen an error while sending the email", result);

            return new ObjectResult(result.Body);
        }
    }
}
