using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "EmailDirectExchange";
        public static string RoutingMail = "email-route";
        public static string QueueName = "queue-email-recommendation";

        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;

        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();


            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            _channel.QueueDeclare(QueueName, true, false, false, null);


            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingMail);

            _logger.LogInformation("RabbitMQ connected...");


            return _channel;

        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMQ disconnected...");

        }
    }
}
