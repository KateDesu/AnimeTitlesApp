using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.ViewModels.AnimeTypes
{
    public class EditAnimeTypeViewModel
    {
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите тип аниме")]
        [Display(Name = "Тип аниме")]
        public string AnimeOfType { get; set; }
    }
}
