using Microsoft.AspNetCore.Mvc;
using Movie_Web.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Movie_Web.Services;
using AutoMapper;
using Movie_Web.Models.DTOs;

namespace Movie_Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly string HostUri;
        private readonly IAPIClientService<Movie> _clientService;
        private readonly IMapper _mapper;
        public HomeController(IAPIClientService<Movie> clientService, IMapper mapper)
        {
            _clientService = clientService;
            HostUri = ConfigurationManager.AppSettings["MovieAPI"]!;
            _mapper = mapper;
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
        public async Task<IActionResult> AddMovieAsync(MovieDetailDTO addMovie)
        {
            var movieModel = _mapper.Map<Movie>(addMovie);
            string Uri = Path.Combine(HostUri + "/api/Movie");
            var movie = await _clientService.PostMovieToAPIAsync(Uri,movieModel);
            return View(movie);
        }
        public async Task<IActionResult> UpdateMovieAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.GetMovieByIdFromAPIAsync(Uri);
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMovieAsync(MovieDetailDTO updateMovie,string id)
        {
            var movieModel = _mapper.Map<Movie>(updateMovie);
            movieModel.Id = id;
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.UpdateMovieToAPIAsync(Uri,movieModel);
            return RedirectToAction("Movie");
        }
        public async Task<IActionResult> UpdateMovieDurationAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.GetMovieByIdFromAPIAsync(Uri);
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMovieDurationAsync(MovieDurationDTO updateMovie,string id)
        {
            var movieModel = _mapper.Map<Movie>(updateMovie);
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.UpdateMovieDurationToAPIAsync(Uri, movieModel);
            return RedirectToAction("Movie");
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