using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimeTitlesApp.Models.Data
{
    public class Genre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage ="Введите жанр")]
        [Display(Name = "Жанр")]
        public string GenreName { get; set; }
    }
}
