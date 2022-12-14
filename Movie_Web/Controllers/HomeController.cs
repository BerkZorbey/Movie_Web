using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movie_Web.Models;
using Movie_Web.Models.DTOs;
using Movie_Web.Services;
using System.Diagnostics;
using ConfigurationManager = System.Configuration.ConfigurationManager;

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
        public async Task<IActionResult> MovieAsync(int currentPage = 1)
        {

            string Uri = Path.Combine(HostUri + "/api/Movie?Page=" + currentPage + "&PageSize=30&Sort=Title&SortingDirection=1");
            var response = await _clientService.GetResponseFromAPIAsync(Uri);
            var movieList = await _clientService.GetModelAsync(response["data"]);
            var paging = await _clientService.GetPagingFromMovieApiAsync(response["paging"]);

            return View(new MoviePaging()
            {
                Movie = movieList,
                Paging = paging
            });
        }

        public async Task<IActionResult> MovieDetailAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.GetModelByIdFromAPIAsync(Uri);
            return View(movie);
        }

        public IActionResult AddMovie() => View();

        [HttpPost]
        public async Task<IActionResult> AddMovieAsync(MovieDetailDTO addMovie)
        {
            var movieModel = _mapper.Map<Movie>(addMovie);
            string Uri = Path.Combine(HostUri + "/api/Movie");
            var movie = await _clientService.PostModelToAPIAsync(Uri, movieModel);
            return View(movie);
        }

        public async Task<IActionResult> UpdateMovieAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.GetModelByIdFromAPIAsync(Uri);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovieAsync(MovieDetailDTO updateMovie, string id)
        {
            var movieModel = _mapper.Map<Movie>(updateMovie);
            movieModel.Id = id;
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.UpdateModelToAPIAsync(Uri, movieModel);
            return RedirectToAction("Movie");
        }

        public async Task<IActionResult> UpdateMovieDurationAsync(string id)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.GetModelByIdFromAPIAsync(Uri);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovieDurationAsync(MovieDurationDTO updateMovie, string id)
        {
            var movieModel = _mapper.Map<Movie>(updateMovie);
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.UpdateModelPartAPIAsync(Uri, movieModel);
            return RedirectToAction("Movie");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteMovieAsync(string id, int currentpage)
        {
            string Uri = Path.Combine(HostUri + "/api/Movie/" + id);
            var movie = await _clientService.DeleteModelAsync(Uri);
            return RedirectToAction("Movie", new { currentpage });
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
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.Token = HttpContext.Session.GetString("Token");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            _clientService.AddHeaderToken(ViewBag.Token);
            base.OnActionExecuting(context);
        }


    }
}