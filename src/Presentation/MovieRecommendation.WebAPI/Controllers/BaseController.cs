using Microsoft.AspNetCore.Mvc;
using MovieRecommendation.Application.Responses;
using System.Net;

namespace MovieRecommendation.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult Error(ServiceResponse response)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }
}
