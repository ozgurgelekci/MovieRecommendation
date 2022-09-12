using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Interfaces.Caching.Redis;
using MovieRecommendation.Domain.Models.Settings;
using MovieRecommendation.Infrastructure.Caching.Redis;
using MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations;
using RabbitMQ.Client;

namespace MovieRecommendation.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure
            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            #endregion

            #region Services
            services.AddScoped(sp => sp.GetService<IOptionsSnapshot<RedisSettings>>().Value);
            services.AddTransient<IRedisConnectionWrapper, RedisConnectionWrapper>();
            services.AddTransient<ICacheManager, RedisCacheManager>();

            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });
            services.AddSingleton<RabbitMQPublisher>();
            services.AddSingleton<RabbitMQClientService>();
            #endregion

            return services;
        }
    }
}
