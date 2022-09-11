using Application.Features.Socials.Commands.CreateSocial;
using Application.Features.Socials.Commands.DeleteSocial;
using Application.Features.Socials.Commands.UpdateSocial;
using Application.Features.Socials.Dtos;
using Application.Features.Socials.Models;
using Application.Features.Socials.Queries.GetByIdSocial;
using Application.Features.Socials.Queries.GetListSocial;
using Application.Features.Socials.Queries.GetListSocialByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSocialCommand createSocialCommand)
        {
            CreatedSocialDto result = await Mediator.Send(createSocialCommand);
            return Created("", result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSocialCommand updateSocialCommand)
        {
            UpdatedSocialDto result = await Mediator.Send(updateSocialCommand);
            return Ok(result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteSocialCommand deleteSocialCommand)
        {
            DeletedSocialDto result = await Mediator.Send(deleteSocialCommand);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSocialQuery getListSocialQuery = new() { PageRequest = pageRequest };
            SocialListModel result = await Mediator.Send(getListSocialQuery);
            return Ok(result);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListSocialByDynamicQuery getListByDynamicSocialQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
            SocialListModel result = await Mediator.Send(getListByDynamicSocialQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdSocialQuery getByIdSocialQuery)
        {
            SocialGetByIdDto socialGetByIdDto = await Mediator.Send(getByIdSocialQuery);
            return Ok(socialGetByIdDto);
        }
    }
}
