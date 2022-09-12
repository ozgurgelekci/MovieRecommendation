using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.Infrastructure.Models;
using MovieRecommendation.Infrastructure.Queues.RabbitMQ.EmailRecommendations;
using System.Net;

namespace MovieRecommendation.WebAPI.Controllers
{
    public class MovieRecommendationsController : BaseController
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public MovieRecommendationsController(RabbitMQPublisher rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Post(EMailRecommendationModel model)
        {
            _rabbitMQPublisher.Publish(model);
            return Ok();
        }
    }
}