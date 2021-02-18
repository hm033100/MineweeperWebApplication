using Microsoft.AspNetCore.Mvc;
using MineweeperWebApplication.Models;
using MineweeperWebApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineweeperWebApplication.Controllers
{
    public class UserController : Controller
    {
        UserService service = new UserService();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterForm()
        {
            return View();
        }

        public IActionResult LoginForm()
        {
            return View();
        }

        public IActionResult ProcessLogin(User user)
        {
            if (service.LoginUser(user))
            {
                return RedirectToAction("Index", "Board");
            }
            else
            {
                return View("LoginFailure", user);
            }
        }

        public IActionResult ProcessRegister(User user)
        {
            if (service.RegisterUser(user))
            {
                return View("RegisterSuccess", user);
            }
            else
            {
                return View("RegisterFailure", user);
            }
        }
    }
}
