using Microsoft.AspNetCore.Mvc;

namespace GD.Finishing.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateAsync(LoginInfo loginInfo)
        {
            var result = await userService.AuthenticateAsync(loginInfo.UserName, loginInfo.Password);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            if (string.IsNullOrWhiteSpace(result.Data.Token))
                return BadRequest("El nombre de usuario o password es incorrecto");

            //return Ok(new { Token = result.Data.Token, UserId = result.Data.UserID });
            return Ok(new { result.Data });
        }

    }
}
