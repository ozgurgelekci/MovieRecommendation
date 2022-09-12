using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using MovieRecommendation.Application.Behaviors;
using System.Reflection;

namespace MovieRecommendation.Application
{
    public static class ConfigureService
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandlingBehavior<,,>));

            services.AddControllers().AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
                conf.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
