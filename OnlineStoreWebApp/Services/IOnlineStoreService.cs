using System.Collections.Generic;
using OnlineStoreWebApp.Models;

namespace OnlineStoreWebApp.Services
{
    public interface IOnlineStoreService
    {
        IList<RegisterViewModel> GetRegisteredCustomers();
        void RegisterCustomer(RegisterViewModel registerViewModel);
    }
}
