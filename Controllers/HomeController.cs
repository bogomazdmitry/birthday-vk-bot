using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BirthdayVkBot.Controllers
{
    
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index","Input login and password");
        }

        [HttpPost]
        public async Task<IActionResult> Index(string returnUrl, string login, string password)
        {
            User user = new User();
            user.login = login;
            user.password = password;
            User currentUser = null;
            using (FileStream fs = new FileStream("user.json", FileMode.Open, FileAccess.Read))
            {
                // десериализация (создание объекта из потока)
                currentUser = (User)await JsonSerializer.DeserializeAsync<User>(fs);
            }
            if (user != null && currentUser != null && currentUser.login == user.login && currentUser.password == user.password)
            {
                await Authenticate(user); // аутентификация

                return Redirect(returnUrl);
            }
            return View("Index", "Incorrect login or password");
        }

        //[Route("/Home/Logout", Name = "logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }


        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.login)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

       
    }
}
