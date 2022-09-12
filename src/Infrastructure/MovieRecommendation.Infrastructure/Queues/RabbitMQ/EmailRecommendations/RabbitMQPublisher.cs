using MovieRecommendation.Infrastructure.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(EMailRecommendationModel eMailRecommendationModel)
        {
            var channel = _rabbitMQClientService.Connect();

            var bodyString = JsonSerializer.Serialize(eMailRecommendationModel);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: RabbitMQClientService.RoutingMail, basicProperties: properties, body: bodyByte);

        }
    }
}
