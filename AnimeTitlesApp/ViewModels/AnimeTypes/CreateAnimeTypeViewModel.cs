using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.ViewModels.AnimeTypes
{
    public class CreateAnimeTypeViewModel
    {
        [Required(ErrorMessage = "Введите тип аниме")]
        [Display(Name = "Тип аниме")]
        public string AnimeOfType { get; set; }
    }
}
