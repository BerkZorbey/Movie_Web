using Microsoft.AspNetCore.Mvc;
using Movie_Web.Models;
using Movie_Web.Models.ApiResponse;
using Movie_Web.Models.Value_Object;
using Movie_Web.Services;
using System.Net.Http.Headers;
using System.Net.Http;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Diagnostics.Eventing.Reader;
using AutoMapper;

namespace Movie_Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly string HostUri;
        private readonly IAPIClientService<User> _clientService;
        private readonly IMapper _map;


        public LoginController(IAPIClientService<User> clientService, IMapper map)
        {
            _clientService = clientService;
            HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
            _map = map;
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

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromForm]LoginApiResponse loginUser)
        {
            var user = _map.Map<User>(loginUser);
            string Uri = Path.Combine(HostUri + "/api/login");
            var response = await _clientService.PostModelToAPIAsync(Uri, user);
            HttpContext.Session.SetString("Token", response.Token.AccessToken);
            HttpContext.Session.SetString("UserName", response.UserName);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
