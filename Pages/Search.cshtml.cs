using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieSearchApp.Models;
using MovieSearchApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearchApp.Pages
{
    public class SearchModel : PageModel
    {
        private readonly MovieService _movieService;

        public SearchModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        public string Query { get; set; }
        public List<Movie> Movies { get; set; }
        public string JsonResponse { get; set; }

        public async Task OnGetAsync(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                Query = query;
                var result = await _movieService.SearchMoviesAsync(query);
                Movies = result.Item1;
                JsonResponse = result.Item2;
            }
            else
            {
                var result = await _movieService.GetPopularMoviesAsync();
                Movies = result.Item1;
                JsonResponse = result.Item2;
            }
        }
    }
}