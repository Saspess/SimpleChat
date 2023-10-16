using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.DTOs.UserChat;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Business.Services.Implementation;

namespace SimpleChat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChatController : ControllerBase
    {
        private readonly IUserChatService _userChatService;

        public UserChatController(IUserChatService userChatService)
        {
            _userChatService = userChatService ?? throw new ArgumentNullException(nameof(userChatService));
        }

        [HttpGet("get-by-chatId/{chatId:int}")]
        public async Task<IActionResult> GetAllUsersByChatIdAsync([FromRoute] int chatId)
        {
            var result = await _userChatService.GetAllUsersByChatIdAsync(chatId);

            return Ok(result);
        }

        [HttpGet("get-by-userId/{userId:int}")]
        public async Task<IActionResult> GetAllChatsByUserIdAsync([FromRoute] int userId)
        {
            var result = await _userChatService.GetAllChatsByUserIdAsync(userId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToChatAsync([FromBody] UserChatCreateDto userChatCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userChatService.AddToChatAsync(userChatCreateDto);

            return StatusCode(201, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userChatService.DeleteFromChatAsync(id);

            return Ok();
        }
    }
}
