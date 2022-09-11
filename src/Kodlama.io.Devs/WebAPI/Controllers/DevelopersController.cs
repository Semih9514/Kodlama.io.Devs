using Application.Features.Developers.Commands.LoginDeveloper;
using Application.Features.Developers.Commands.RegisterDeveloper;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopersController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterDeveloperCommand command)
        {
            AccessToken sendedData = await Mediator.Send(command);
            return Ok(sendedData);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDeveloperCommand command)
        {
            var sendedData = await Mediator.Send(command);
            return Ok(sendedData);
        }
    }
}
