using Microsoft.AspNetCore.Mvc;
using OnlineStoreWebApp.Models;
using OnlineStoreWebApp.Services;

namespace OnlineStoreWebApp.Controllers
{
    public class RegisterController : Controller
    {
        readonly IOnlineStoreService _onlineStoreService;

        public RegisterController(IOnlineStoreService onlineStoreService)
        {
            _onlineStoreService = onlineStoreService;
        }

        public IActionResult Index()
        {
            var registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(RegisterViewModel registerViewModel)

        {
            if (ModelState.IsValid)
            {

                _onlineStoreService.RegisterCustomer(registerViewModel);
                return View("RegistrationConfirmation"); 
            }

            return View(registerViewModel);
        }
    }
}
