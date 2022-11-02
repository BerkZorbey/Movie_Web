using Microsoft.AspNetCore.Mvc;
using Movie_Web.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Movie_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string HostUri;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
        }
        public async Task<IActionResult> IndexAsync()
        {
            string Uri = Path.Combine(HostUri + "/api/Movie?Page=1&PageSize=10&Sort=Title&SortingDirection=1");
            List<Movie> movies = new List<Movie>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(Uri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<List<Movie>>(apiResponse);
                }
            }
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}