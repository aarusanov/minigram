namespace Minigram.Auth.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Minigram.Auth.Dto;
    using Minigram.Auth.Dto.User;
    using Minigram.Auth.Services;
    using Minigram.Core.Models;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<PagedResponse<ReadUserDto>> GetAll([FromQuery] QueryParams queryParams)
        {
            ArgumentNullException.ThrowIfNull(queryParams);

            int count = await _userService.Count();
            List<ReadUserDto> data = await _userService.GetAll(queryParams);

            return new PagedResponse<ReadUserDto>
            {
                Count = count,
                Data = data,
            };
        }

        [HttpGet("{id}")]
        public async Task<ReadUserDto> Get([FromRoute] Guid id)
        {
            return await _userService.Get(id);
        }

        [HttpGet("count")]
        public async Task<int> Count()
        {
            return await _userService.Count();
        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            return CreatedAtAction(nameof(Get), user.Id);
        }

        [HttpPut]
        public ActionResult Update([FromBody] User user)
        {
            return CreatedAtAction(nameof(Get), user.Id);
        }

        [HttpPatch("{id}")]
        public ActionResult Patch([FromRoute] Guid id, [FromBody] User user)
        {
            return CreatedAtAction(nameof(Get), id);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id)
        {
            await _userService.Delete(id);
        }
    }
}
