using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Service.ViewModels;

namespace AutenticacionApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly IAuthentication _authentication;

        public AutenticacionController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var result = await _authentication.LogIn(loginViewModel);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { Token = result.Token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerView)
        {
            var result = await _authentication.SignIn(registerView);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
