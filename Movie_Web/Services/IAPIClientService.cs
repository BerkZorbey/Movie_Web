using Movie_Web.Models;

namespace Movie_Web.Services
{
    public interface IAPIClientService<TEntity> where TEntity : BaseEntity 
    {
       public Task<List<TEntity>> GetMoviesFromAPIAsync(string Uri);
       public Task<TEntity> GetMovieByIdFromAPIAsync(string Uri);
    }
}
