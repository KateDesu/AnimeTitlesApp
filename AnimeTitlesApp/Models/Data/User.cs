using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AnimeTitlesApp.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите фамилию")]

        //отображение Фамилия вместо LastName
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }


        //навигационные свойства
    }
}
