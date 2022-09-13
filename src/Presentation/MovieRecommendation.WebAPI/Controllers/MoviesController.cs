using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.Application.Features.Queries.Movies.GetByIdMovie;
using ProductExample.Application.Features.Queries.GetAllProduct;
using System.Net;

namespace MovieRecommendation.WebAPI.Controllers
{
    public class MoviesController : BaseController
    {
        IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetAllMovieQueryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] GetAllMovieQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(GetByIdMovieQueryResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetByIdMovieQueryResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(GetByIdMovieQueryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] GetByIdMovieQueryRequest request)
        {
            var result = await _mediator.Send(request);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}