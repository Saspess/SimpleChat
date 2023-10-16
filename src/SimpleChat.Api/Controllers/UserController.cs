using Microsoft.AspNetCore.Mvc;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Business.Services.Contracts;

namespace SimpleChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _userService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("get-by-email/{email}")]
        public async Task<IActionResult> GetByEmailAsync([FromRoute] string email)
        {
            var result = await _userService.GetByEmailAsync(email);

            return Ok(result);
        }

        [HttpGet("get-by-username/{username}")]
        public async Task<IActionResult> GetByUsernameAsync([FromRoute] string username)
        {
            var result = await _userService.GetByUsernameAsync(username);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.CreateAsync(userCreateDto);

            return StatusCode(201, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.UpdateAsync(userUpdateDto);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userService.DeleteAsync(id);

            return Ok();
        }
    }
}
