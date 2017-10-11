using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreWebApi.Models;
using OnlineStoreWebApi.Services;

namespace OnlineStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        private readonly ICustomerService _customerService;
        public RegisterController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IList<Customer> Get()
        {
            return _customerService.GetRegisteredCustomers();
        }

        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            _customerService.RegisterCustomer(customer);
        }     
    }
}
