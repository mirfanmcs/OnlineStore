using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineStoreWebApi.Models;
using OnlineStoreWebApi.Settings;
using RabbitMQ.Client;

namespace OnlineStoreWebApi.Messaging
{
    public class OnlineStoreMq : IOnlineStoreMq
    {
        readonly IOptions<OnlineStoreMqSettings> _onlineStoreMqSettings;
        public OnlineStoreMq(IOptions<OnlineStoreMqSettings> onlineStoreMqSettings)
        {
            _onlineStoreMqSettings = onlineStoreMqSettings;
        }

        public void SendMessage(Customer customer)
        {
            var onlineStoreMqUserName = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_USERNAME");
            var onlineStoreMqPassword = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_PASSWORD");
            var onlineStoreMqServer = Environment.GetEnvironmentVariable("ONLINE_STORE_MQ_SERVER");

            var factory = new ConnectionFactory() 
            { HostName = onlineStoreMqServer, UserName = onlineStoreMqUserName, Password = onlineStoreMqPassword };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.ExchangeDeclare(_onlineStoreMqSettings.Value.ExchangeName, _onlineStoreMqSettings.Value.ExchhangeType);

                channel.QueueDeclare(queue: _onlineStoreMqSettings.Value.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueBind(queue: _onlineStoreMqSettings.Value.QueueName, exchange: _onlineStoreMqSettings.Value.ExchangeName, routingKey: _onlineStoreMqSettings.Value.RouteKey);

                string customerData = JsonConvert.SerializeObject(customer);
                var body = Encoding.UTF8.GetBytes(customerData);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: _onlineStoreMqSettings.Value.ExchangeName,
                                     routingKey: _onlineStoreMqSettings.Value.RouteKey,
                                     basicProperties: properties,
                                     body: body);
            }

        }
    }
}
