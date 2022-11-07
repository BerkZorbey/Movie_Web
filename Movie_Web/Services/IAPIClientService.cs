using Movie_Web.Models;

namespace Movie_Web.Services
{
    public interface IAPIClientService<TEntity> where TEntity : BaseEntity 
    {
       Task<List<TEntity>> GetMoviesFromAPIAsync(string Uri);
       Task<TEntity> GetMovieByIdFromAPIAsync(string Uri);
       Task<TEntity> PostMovieToAPIAsync(string Uri, TEntity addMovie);
       Task<TEntity> UpdateMovieToAPIAsync(string Uri, TEntity addMovie);
       Task<TEntity> UpdateMovieDurationToAPIAsync(string Uri, TEntity updateMovieDuration);
       Task<TEntity> DeleteMovieAsync(string Uri);
    }
}
