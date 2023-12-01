using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.ViewModels.AnimeTitles
{
    public class EditAnimeTitleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите оригинальное название аниме")]
        [Display(Name = "Оригинальное название")]
        public string OrigName { get; set; }

        [Required(ErrorMessage = "Введите название аниме")]
        [Display(Name = "Название")]
        public string TitleName { get; set; }

        [Display(Name = "Год выпуска")]
        public int? YearOfIssue { get; set; }

        [Display(Name = "Описание")]
        public string? Descr { get; set; }

        [Display(Name = "Постер")]
        public string? Poster { get; set; }

        [Display(Name = "Количество серий")]
        public short? CountSeries { get; set; }

        [Display(Name = "Продолжительность")]
        public short? Duration { get; set; }

        [Required(ErrorMessage = "Завершено ли аниме")]
        [Display(Name = "Завершенность")]
        public bool IsComplete { get; set; }

        [Required(ErrorMessage = "Введите студию")]
        [Display(Name = "Студия")]
        public string Studio { get; set; }

        [Required]
        [Display(Name = "Тип аниме")]
        public short IdAnimeTitle { get; set; }

    }
}
