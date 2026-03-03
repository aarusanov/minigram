namespace Minigram.Auth.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Minigram.Auth.Services;

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }
    }
}
