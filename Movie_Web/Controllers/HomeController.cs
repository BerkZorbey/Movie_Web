using Microsoft.AspNetCore.Mvc;
using Movie_Web.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Movie_Web.Services;

namespace Movie_Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly string HostUri;
        private readonly IAPIClientService<Movie> _clientService;
        public HomeController(IAPIClientService<Movie> clientService)
        {
            _clientService = clientService;
           HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
        }
        public IActionResult IndexAsync()
        {
            return View();
        }
        public async Task<IActionResult> MovieAsync()
        {
            List<Movie> movies = new();
            string Uri = Path.Combine(HostUri + "/api/Movie?Page=1&PageSize=10&Sort=Title&SortingDirection=1");
            movies = await _clientService.GetMoviesFromAPIAsync(Uri);

            return View(movies);
        }
        public async Task<IActionResult> MovieDetailAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/"+id);
            var movie = await _clientService.GetMovieByIdFromAPIAsync(Uri);
            return View(movie);
        }
        
        public IActionResult AddMovie() => View();
        [HttpPost]
        public async Task<IActionResult> AddMovieAsync(Movie addMovie)
        {
            
            string Uri = Path.Combine(HostUri + "/api/Movie");
            var movie = await _clientService.PostMovieToAPIAsync(Uri,addMovie);
            return View(movie);
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