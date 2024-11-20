using Application.Contracts;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> LoginUserIn(LoginDTO loginDTO)
        {
            var result = await _user.LoginUserAsync(loginDTO);

            if (result.Success)
                return Ok(result);  

            return BadRequest(result); 
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<RegistrationResponse>>> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var result = await _user.RegisterUserAsync(registerUserDTO);

            if (result.Success)
                return Ok(result); 
            
            return BadRequest(result); 
        }
    }
}
