using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieSearchApp.Models;
using MovieSearchApp.Services;
using System.Threading.Tasks;

namespace MovieSearchApp.Pages
{
    public class MovieDetailsModel : PageModel
    {
        private readonly MovieService _movieService;

        public MovieDetailsModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(string title)
        {
            var result = await _movieService.SearchMoviesAsync(title); // Utiliza tu método para buscar películas por título

            if (result.Item1.Count > 0)
            {
                Movie = result.Item1[0]; // Tomamos la primera película encontrada
                return Page();
            }
            else
            {
                return NotFound(); // Manejo básico si no se encuentra la película
            }
        }
    }
}
