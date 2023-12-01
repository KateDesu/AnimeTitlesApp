using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace AnimeTitlesApp.Models.Data
{
    public class AnimeType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Введите тип аниме")]
        [Display(Name = "Тип аниме")]
        public string AnimeOfType { get; set; }


        // Навигационные свойства
        public ICollection<AnimeTitle> AnimeTitles { get; set; }
    }
}
