using EmailService;
using Movie_Web.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Movie_Web.Services
{
    public class MailService
    {
        private readonly string HostUri;
        private readonly string WebUri;
        private readonly IEmailSender _emailSender;
        private readonly IAPIClientService<UserEmailVerificationModel> _clientEmailService;
        public MailService(IAPIClientService<UserEmailVerificationModel> clientEmailService, IEmailSender emailSender)
        {
            WebUri = ConfigurationManager.AppSettings["MovieWeb"]!;
            HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
            _clientEmailService = clientEmailService;
            _emailSender = emailSender;
        }
        public async void SendEmail(User response)
        {
            string EmailVerificationUri = Path.Combine(HostUri + "/api/register/" + response.Id);
            var responseEmailToken = await _clientEmailService.GetModelByIdFromAPIAsync(EmailVerificationUri);
            string activationUri = Path.Combine(WebUri + "/Login/EmailActivate?id=" + response.Id + "&VerificationToken=" + responseEmailToken.EmailVerificationToken.AccessToken);
            var message = new Message(new string[] { response.Email }, "E-mail activation", $"This is the activation Uri from our email. '{activationUri}'");
            _emailSender.SendEmail(message);
        }
    }
}
