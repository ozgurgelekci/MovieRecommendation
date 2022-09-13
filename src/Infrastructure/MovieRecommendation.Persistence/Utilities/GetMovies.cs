using AutoMapper;
using MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Repositories;
using MovieRecommendation.Domain.Entities;
using MovieRecommendation.Persistence.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MovieRecommendation.Persistence.Utilities
{
    public static class GetMovies
    {

        public static async Task Invoke(IMovieRepository movieRepository, ICacheManager cacheManager, IMapper mapper)
        {
            var count = 50;
            int page = 1;

            var externalUrl = "https://api.themoviedb.org/3/discover/movie";
            var apiKey = "b05982a82397471876217cc0f97ae4ba";

            //List<Movie> moviesForBulkInsert = new();

            for (int i = 0; i < count; i++)
            {
                var url = $"{externalUrl}?api_key={apiKey}&page={page}";
                var client = new RestClient(url);

                var request = new RestRequest(url, Method.Get);
                RestResponse response = client.Execute(request);
                var output = response.Content;

                var deserializeOutput = JsonConvert.DeserializeObject<ResultMovie>(output);
                var movies = deserializeOutput.Movies;
                if (movies == null)
                {
                    break;
                }

                foreach (var movie in movies)
                {
                    var getMovie = movieRepository.GetById(movie.Id);

                    if (getMovie == null)
                    {
                        Movie movieEntity = new Movie
                        {
                            Id = movie.Id,
                            Title = movie.Title,
                            VoteAverage = 0,
                            VoteCount = 0
                        };

                        //moviesForBulkInsert.Add(movieEntity);

                        bool isCache = cacheManager.IsSet($"{movie.Id}");
                        var mappedMovie = mapper.Map<GetByIdMovieQueryResponse>(movieEntity);

                        if (!isCache)
                            cacheManager.Set($"{movie.Id}", mappedMovie, 360000);

                        await movieRepository.AddAsync(movieEntity);
                    }
                }

                page++;
            }
            //if (moviesForBulkInsert != null)
            //    await _movieRepository.BulkInsertAsync(moviesForBulkInsert);
        }
    }
}
