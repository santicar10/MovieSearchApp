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
            var result = await _movieService.SearchMoviesAsync(title); // Utiliza tu m�todo para buscar pel�culas por t�tulo

            if (result.Item1.Count > 0)
            {
                Movie = result.Item1[0]; // Tomamos la primera pel�cula encontrada
                return Page();
            }
            else
            {
                return NotFound(); // Manejo b�sico si no se encuentra la pel�cula
            }
        }
    }
}
