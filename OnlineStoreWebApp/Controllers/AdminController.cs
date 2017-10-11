using Microsoft.AspNetCore.Mvc;
using OnlineStoreWebApp.Models;
using OnlineStoreWebApp.Services;

namespace OnlineStoreWebApp.Controllers
{
    public class Admin : Controller
    {
        readonly IOnlineStoreService _onlineStoreService;

        public Admin(IOnlineStoreService onlineStoreService)
        {
            _onlineStoreService = onlineStoreService;
        }
        public IActionResult Index()
       
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            adminViewModel.RegisteredCustomers = _onlineStoreService.GetRegisteredCustomers();
            return View(adminViewModel);
        }
    }
}
