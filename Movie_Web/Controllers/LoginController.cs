using AutoMapper;
using EmailService;
using Microsoft.AspNetCore.Mvc;
using Movie_Web.Models;
using Movie_Web.Models.ApiResponse;
using Movie_Web.Services;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Movie_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly string HostUri;
        private readonly IAPIClientService<User> _clientService;
        private readonly IMapper _map;
        private readonly MailService _mailService;


        public LoginController(IAPIClientService<User> clientService, IMapper map, MailService mailService)
        {
            _clientService = clientService;
            HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
            _map = map;
            _mailService = mailService;
        }

        public IActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterApiResponse registerUser)
        {
            var user = _map.Map<User>(registerUser);
            string Uri = Path.Combine(HostUri + "/api/register");
            var response = await _clientService.PostModelToAPIAsync(Uri, user);
            _mailService.SendEmail(response);
            
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm] LoginApiResponse loginUser)
        {
            var user = _map.Map<User>(loginUser);
            string Uri = Path.Combine(HostUri + "/api/login");
            var response = await _clientService.PostModelToAPIAsync(Uri, user);
            if(response.isActivatedEmail == false)
            {
                return BadRequest();
            }
            HttpContext.Session.SetString("Token", response.Token.AccessToken);
            HttpContext.Session.SetString("UserName", response.UserName);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> EmailActivate([FromQuery] EmailActivateModel activateModel)
        {
            string Uri = Path.Combine(HostUri + "/api/activatemail/id=" + activateModel.Id + "&VerificationToken=" + activateModel.VerificationToken);
            var response = await _clientService.GetModelByIdFromAPIAsync(Uri);
            return View(response);
        }
        [HttpPost]
        public IActionResult EmailActivate(string Id,string Email)
        {
            User user = new();
            user.Id = Id;
            user.Email = Email;
            _mailService.SendEmail(user);
            return RedirectToAction("Index", "Home");
        }

    }
}
