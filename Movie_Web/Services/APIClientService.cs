using Movie_Web.Models;
using Newtonsoft.Json;

namespace Movie_Web.Services
{
    public class APIClientService<TEntity> : IAPIClientService<TEntity> where TEntity : BaseEntity
    {
        private readonly HttpClient _httpClient;
        public APIClientService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<TEntity>> GetMoviesFromAPIAsync(string Uri)
        {
            using (var response = await _httpClient.GetAsync(Uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TEntity>>(apiResponse);
            } 
        }
        public async Task<TEntity> GetMovieByIdFromAPIAsync(string Uri)
        {
            using (var response = await _httpClient.GetAsync(Uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
    }
}
