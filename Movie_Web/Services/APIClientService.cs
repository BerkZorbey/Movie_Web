﻿using Movie_Web.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

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
        public async Task<TEntity> PostMovieToAPIAsync(string Uri,TEntity addMovie)
        {
            var JsonMovie = JsonConvert.SerializeObject(addMovie);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");
            
            using (var response = await _httpClient.PostAsync(Uri,stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> UpdateMovieToAPIAsync(string Uri, TEntity updateMovie)
        {
            var JsonMovie = JsonConvert.SerializeObject(updateMovie);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync(Uri, stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> UpdateMovieDurationToAPIAsync(string Uri, TEntity updateMovieDuration)
        {
            var JsonMovie = JsonConvert.SerializeObject(updateMovieDuration);
            StringContent stringContent = new StringContent(JsonMovie, Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PatchAsync(Uri, stringContent))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
        public async Task<TEntity> DeleteMovieAsync(string Uri)
        {
            using (var response = await _httpClient.DeleteAsync(Uri))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TEntity>(apiResponse);
            }
        }
    }
}
