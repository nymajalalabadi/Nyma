using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nyma.Application.Services.Interfaces;
using Nyma.Domain.Models;
using Nyma.Domain.ViewModels.Page;
using Nyma.Domain.ViewModels.User;
using Nyma.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nyma.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor

        private readonly IThingIDoService _thingIDoService;

        private readonly ICustomerFeedBackService _customerFeedBackService;

        private readonly ICustomerLogoService _customerLogoServiceService;

        private readonly IUserService _userService;

        public HomeController(IThingIDoService thingIDoService, ICustomerFeedBackService customerFeedBackService, 
            ICustomerLogoService customerLogoServiceService, IUserService userService)
        {
            _thingIDoService = thingIDoService;
            _customerFeedBackService = customerFeedBackService;
            _customerLogoServiceService = customerLogoServiceService;
            _userService = userService;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            IndexPageViewModel index = new IndexPageViewModel()
            {
                ThingIDoList = await _thingIDoService.GetAllThingIDoForIndex(),
                CustomerFeedBackList = await _customerFeedBackService.GetCustomerFeedBackForIndex(),
                CustomerLogoList = await _customerLogoServiceService.GetCustomerLogoForIndex(),
            };

            return View(index);
        }

        #region Login

        [HttpGet("login")]
        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userService.GetUserForLogin(login);

            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };

                    await HttpContext.SignInAsync(principal, properties);

                    return RedirectToAction("Index" , "Home");
                }

                else
                {
                    ModelState.AddModelError("UserName", "حساب کاربری شما فعال نمی باشد");
                }

            }

            ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
            return Redirect("/");
        }

        #endregion

        #region Log Out 

        [HttpGet("log-out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }

        #endregion

    }
}
