using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.ViewModels.Genres
{
    public class CreateGenreViewModel
    {
        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string GenreName { get; set; }
    }
}
