using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.Application.Constants.Authentication;
using MovieRecommendation.Application.Features.Commands.MovieVotes;
using MovieRecommendation.Application.Interfaces.Caching;
using MovieRecommendation.Application.Responses;
using System.Net;
using System.Security.Claims;

namespace MovieRecommendation.WebAPI.Controllers
{

    
    public class MovieVotesController : BaseController
    {
        IMediator _mediator;
        readonly ICacheManager cacheManager;

        public MovieVotesController(IMediator mediator, ICacheManager cacheManager)
        {
            _mediator = mediator;
            this.cacheManager = cacheManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            cacheManager.Clear();

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(CreateMovieVoteCommandRequest request)
        {
            var userId = User.FindFirstValue(ClaimName.Id);
            request.UserId = int.Parse(userId);

            var result = await _mediator.Send(request);

            return !result.Success
            ? Error(result)
            : Ok(result);
        }
    }
}