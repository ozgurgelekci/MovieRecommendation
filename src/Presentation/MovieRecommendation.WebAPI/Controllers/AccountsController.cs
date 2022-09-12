using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.Application.Features.Queries.Accounts.Login;
using System.Net;

namespace MovieRecommendation.WebAPI.Controllers
{
    public class AccountsController : BaseController
    {
        IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginQueryResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginQueryRequest request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
