using MovieRecommendation.Domain.Models.Settings;
using MovieRecommendation.EmailConsumer;
using MovieRecommendation.EmailConsumer.Services;
using MovieRecommendation.EmailConsumer.Services.Abstract;
using MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
        services.AddSingleton<RabbitMQClientService>();

        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
