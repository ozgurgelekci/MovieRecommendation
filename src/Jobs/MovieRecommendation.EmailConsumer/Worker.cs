using MovieRecommendation.EmailConsumer.Services.Abstract;
using MovieRecommendation.Infrastructure.Models;
using MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MovieRecommendation.EmailConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        readonly IEmailService _eMailService;

        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IEmailService eMailService)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
            _eMailService = eMailService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                AsyncEventingBasicConsumer consumer = new(_channel);

                _channel.BasicConsume(
                    queue: RabbitMQClientService.QueueName,
                    autoAck: false,
                    consumer: consumer
                    );

                consumer.Received += Consumer_Received;

                await Task.Delay(600000, stoppingToken);
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var mailInfo = JsonSerializer.Deserialize<EMailRecommendationModel>(Encoding.UTF8.GetString(@event.Body.ToArray()));

           await _eMailService.SendAsync(mailInfo);

            _channel.BasicAck(
                deliveryTag: @event.DeliveryTag,
                multiple: false);

        }
    }
}