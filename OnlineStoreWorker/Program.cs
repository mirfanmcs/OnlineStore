using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStoreWorker.Messaging;
using OnlineStoreWorker.Repositories;
using OnlineStoreWorker.Settings;

namespace OnlineStoreWorker
{
    class Program
    {
        static IConfiguration Configuration;

        static void Main(string[] args)
        {
            ConfigureEnvironment();
            var serviceProvider = ConfigureServices();

            var onlineStoreMq = serviceProvider.GetService<IOnlineStoreMq>();

            Console.WriteLine("Starting to read from the queue");

            while (true)
            {
                onlineStoreMq.ConsumeMessage();
            }

        }

        static void ConfigureEnvironment()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", optional: false);

            Configuration = builder.Build();
        }

        static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();

            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IOnlineStoreMq, OnlineStoreMq>();

            services.Configure<OnlineStoreDbSettings>(Configuration.GetSection("OnlineStoreDbSettings"));
            services.Configure<OnlineStoreMqSettings>(Configuration.GetSection("OnlineStoreMqSettings"));

            return services.BuildServiceProvider();
        }
    }
}
