using AnimeTitlesApp.Models.Data;
using AnimeTitlesApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnimeTitlesApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        // метод срабатывает при открытии страницы регистрации, никакие значения пока передавать не нужно
        // вы просто открыли страницу с регистрацией и не успели еще ничего ввести
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // теперь вы ввели значения и нажали кнопку "Зарегистрироваться", например
        // методом Post данные передаются через модель для представления RegisterViewModel
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // создание экземпляра user класса User и установка его свойствам значениям из модели
                User user = new User
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Email = model.Email,
                    UserName = model.Email
                };

                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);   // возвращение модели в представление
        }
    }
}
