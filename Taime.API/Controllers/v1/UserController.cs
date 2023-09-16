using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Taime.Application.Data.MySql.Entities;
using Taime.Application.Services;
using Taime.Application.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Taime.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtem todos os usu�rios
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para obter os usu�rios.
        ///
        ///     Request:
        ///     GET v1/users
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno dos usu�rios</returns>
        [HttpGet()]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.GetAll();
            return HttpHelper.Convert(response);
        }

        /// <summary>
        /// Obtem um usu�rio por id
        /// </summary>
        /// <remarks>
        /// Exemplo de requisi��o para obter um usu�rio por id.
        ///
        ///     Request:
        ///     GET v1/users/1
        /// </remarks>
        /// <response code="200">Retorno de sucesso</response>
        /// <returns>Retorno do usu�rio</returns>
        /// 
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _userService.GetById(id);
            return HttpHelper.Convert(response);
        }

        [HttpPost()]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Create([FromBody] UserEntity request)
        {
            var response = await _userService.Create(request);
            return HttpHelper.Convert(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // deve ser adicionado as regras aceitas separados por virgula exemplo [Authorize(Roles = "Regra1,Regra2,Regra3")]
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(object)),
        SwaggerResponse((int)HttpStatusCode.Unauthorized, Type = typeof(object))]
        public async Task<IActionResult> Remove([FromRoute] int id)
        {
            var response = await _userService.Remove(id);
            return HttpHelper.Convert(response);
        }
    }
}