using RabbitMQ.Client;

namespace MovieRecommendation.EmailConsumer.Services
{
    public class RabbitMQConsumerService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;


        public static string QueueName = "queue-email-recommendation";

        private readonly ILogger<RabbitMQConsumerService> _logger;

        public RabbitMQConsumerService(ConnectionFactory connectionFactory, ILogger<RabbitMQConsumerService> logger)
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
