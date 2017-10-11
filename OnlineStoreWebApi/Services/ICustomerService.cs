using System.Collections.Generic;
using OnlineStoreWebApi.Models;

namespace OnlineStoreWebApi.Services
{
    public interface ICustomerService
    {
        IList<Customer> GetRegisteredCustomers();
        void RegisterCustomer(Customer customer);
    }
}
