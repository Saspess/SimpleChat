using Microsoft.AspNetCore.Mvc;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.Services.Contracts;

namespace SimpleChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _chatService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("get-by-creatorId/{creatorId:int}")]
        public async Task<IActionResult> GetAllByCreatorIdAsync([FromRoute] int creatorId)
        {
            var result = await _chatService.GetAllByCreatorIdAsync(creatorId);

            return Ok(result);
        }

        [HttpGet("get-by-name/{name}")]
        public async Task<IActionResult> GetAllByNameAsync([FromRoute] string name)
        {
            var result = await _chatService.GetAllByNameAsync(name);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _chatService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ChatCreateDto chatCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _chatService.CreateAsync(chatCreateDto);

            return StatusCode(201, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ChatUpdateDto chatUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _chatService.UpdateAsync(chatUpdateDto);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] ChatDeleteDto chatDeleteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _chatService.DeleteAsync(chatDeleteDto);

            return Ok();
        }
    }
}
