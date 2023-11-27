using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.ViewModels.Genres
{
    public class EditGenreViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите жанр")]
        [Display(Name = "Жанр")]
        public string GenreName { get; set; }
    }
}
