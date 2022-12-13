using Movie_Web.Models;
using System.Text.Json.Nodes;

namespace Movie_Web.Services
{
    public interface IAPIClientService<TEntity> where TEntity : BaseEntity 
    {
       void AddHeaderToken(string token);
       Task<JsonNode> GetResponseFromAPIAsync(string Uri);
       Task<List<TEntity>> GetModelAsync(JsonNode apiResponse);
       Task<TEntity> GetModelByIdFromAPIAsync(string Uri);
       Task<TEntity> PostModelToAPIAsync(string Uri, TEntity addModel);
       Task<TEntity> UpdateModelToAPIAsync(string Uri, TEntity updateModel);
       Task<TEntity> UpdateModelPartAPIAsync(string Uri, TEntity updateModelPart);
       Task<TEntity> DeleteModelAsync(string Uri);
       Task<PagingModel> GetPagingFromMovieApiAsync(JsonNode apiResponse);
    }
}
