using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using OnlineStoreWebApp.Models;
using Microsoft.Extensions.Options;
using OnlineStoreWebApp.Settings;

namespace OnlineStoreWebApp.Services
{
    public class OnlineStoreService : IOnlineStoreService
    {
        readonly IOptions<OnlineStoreApiSettings> _onlineStoreApiSettings;
        public OnlineStoreService(IOptions<OnlineStoreApiSettings> onlineStoreApiSettings)
        {
            _onlineStoreApiSettings = onlineStoreApiSettings;
        }
        public IList<RegisterViewModel> GetRegisteredCustomers()
        {
            var client = GetHttpClient();
            var path = "api/register";

            HttpResponseMessage response = client.GetAsync(path).Result;
            string stringData = response.Content.ReadAsStringAsync().Result;
            List<RegisterViewModel> registeredCustomers = JsonConvert.DeserializeObject<List<RegisterViewModel>>(stringData);
            return registeredCustomers;
        }

        public void RegisterCustomer(RegisterViewModel registerViewModel)
        {
            var client = GetHttpClient();
            var path = "api/register";

            string stringData = JsonConvert.SerializeObject(registerViewModel);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");
            HttpResponseMessage response = client.PostAsync(path, contentData).Result;
        }

        private HttpClient GetHttpClient()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri(_onlineStoreApiSettings.Value.Url);


            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
