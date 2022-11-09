using Movie_Web.Models;
using System.Text.Json.Nodes;

namespace Movie_Web.Services
{
    public interface IAPIClientService<TEntity> where TEntity : BaseEntity 
    {
       Task<JsonNode> GetResponseFromAPIAsync(string Uri);
       Task<List<TEntity>> GetMoviesAsync(JsonNode apiResponse);
       Task<PagingModel> GetPagingAsync(JsonNode apiResponse);
       Task<TEntity> GetMovieByIdFromAPIAsync(string Uri);
       Task<TEntity> PostMovieToAPIAsync(string Uri, TEntity addMovie);
       Task<TEntity> UpdateMovieToAPIAsync(string Uri, TEntity addMovie);
       Task<TEntity> UpdateMovieDurationToAPIAsync(string Uri, TEntity updateMovieDuration);
       Task<TEntity> DeleteMovieAsync(string Uri);
    }
}
