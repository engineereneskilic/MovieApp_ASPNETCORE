using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {

            return View();
        }

        // Remote validations
        [AcceptVerbs("GET","POST")]
        public IActionResult VerifyUserName(string username)
        {
            var users = new List<string> { "sadikturan", "cinarturan" };

            if (users.Any(i => i == username))
            {
                //ModelState.AddModelError(nameof(username), "Zaten bu kullanıcı adı daha önce alındı.");
                return Json($"zaten {username} kullanıcı adı adaha önce alınmış");
            }
            return Json(true);
        }
    }
}
