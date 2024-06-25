using MovieSearchApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MovieSearchApp.Services
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "290b55bb11aba301c50a71f575df3ec8"; 
        private readonly string _imageBaseUrl = "https://image.tmdb.org/t/p/w500";

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(List<Movie>, string)> SearchMoviesAsync(string query)
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={query}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var searchResult = JsonSerializer.Deserialize<TmdbSearchResult>(jsonResponse, options);

            var movies = searchResult.Results.Select(m => new Movie
            {
                Title = m.Title,
                PosterPath = string.IsNullOrEmpty(m.poster_path) ? null : $"{_imageBaseUrl}{m.poster_path}",
                ReleaseDate = string.IsNullOrEmpty(m.release_date) ? "N/A" : m.release_date,
                Overview = m.Overview,
                VoteAverage = m.vote_average
            }).ToList();

            return (movies, jsonResponse);
        }

        public async Task<(List<Movie>, string)> GetPopularMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var popularResult = JsonSerializer.Deserialize<TmdbSearchResult>(jsonResponse, options);

            var movies = popularResult.Results.Select(m => new Movie
            {
                Title = m.Title,
                PosterPath = string.IsNullOrEmpty(m.poster_path) ? null : $"{_imageBaseUrl}{m.poster_path}",
                ReleaseDate = string.IsNullOrEmpty(m.release_date) ? "N/A" : m.release_date,
                Overview = m.Overview,
                VoteAverage = m.vote_average
            }).ToList();

            return (movies, jsonResponse);
        }

        private class TmdbSearchResult
        {
            public List<TmdbMovie> Results { get; set; }
        }

        private class TmdbMovie
        {
            public string Title { get; set; }
            public string poster_path { get; set; }
            public string release_date { get; set; }
            public string Overview { get; set; }
            public double vote_average { get; set; }
        }
    }
}

//290b55bb11aba301c50a71f575df3ec8