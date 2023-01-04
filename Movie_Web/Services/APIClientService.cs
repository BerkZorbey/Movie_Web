using Movie_Web.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace Movie_Web.Services
{
    public class APIClientService<TEntity> : IAPIClientService<TEntity> where TEntity : BaseEntity
    {
        private readonly HttpClient _httpClient;

        public APIClientService()
        {
            _httpClient = new HttpClient();

        }
        public void AddHeaderToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        public Task<List<TEntity>> GetModelAsync(JsonNode apiResponse)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<List<TEntity>>(apiResponse.ToString()));
        }
        public async Task<TEntity> GetModelByIdFromAPIAsync(string Uri)
        {
            using (var response = await _httpClient.GetAsync(Uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> PostModelToAPIAsync(string Uri, TEntity addModel)
        {
            var JsonMovie = JsonConvert.SerializeObject(addModel);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(Uri, stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> UpdateModelToAPIAsync(string Uri, TEntity updateModel)
        {
            var JsonMovie = JsonConvert.SerializeObject(updateModel);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync(Uri, stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> UpdateModelPartAPIAsync(string Uri, TEntity updateModelPart)
        {
            var JsonMovie = JsonConvert.SerializeObject(updateModelPart);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PatchAsync(Uri, stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> DeleteModelAsync(string Uri)
        {
            using (var response = await _httpClient.DeleteAsync(Uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<JsonNode> GetResponseFromAPIAsync(string Uri)
        {
            using (var response = await _httpClient.GetAsync(Uri))
            {
                var apiResponse = JsonObject.Parse(response.Content.ReadAsStringAsync().Result);
                return apiResponse;
            }
        }
        public Task<PagingModel> GetPagingFromMovieApiAsync(JsonNode apiResponse)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<PagingModel>(apiResponse.ToString()));
        }

    }
}
