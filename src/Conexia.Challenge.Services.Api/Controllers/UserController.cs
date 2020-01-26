using Conexia.Challenge.Application.Requests;
using Conexia.Challenge.Application.Users.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Conexia.Challenge.Services.Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
            [FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _userAppService.LoginAsync(
                request.Email, request.Password));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> RecoverById(int id) =>
            Ok(await _userAppService.RecoverByIdAsync(id));
    }
}