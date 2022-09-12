using MovieRecommendation.Infrastructure.Models;

namespace MovieRecommendation.EmailConsumer.Services.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(EMailRecommendationModel request);
    }
}
