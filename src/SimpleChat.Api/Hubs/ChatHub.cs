using Microsoft.AspNetCore.SignalR;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Contracts;

namespace SimpleChat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserChatService _userChatService;

        public ChatHub(IUserChatService userChatService, IChatService chatService, IUserService userService)
        {
            _userChatService = userChatService ?? throw new ArgumentNullException(nameof(userChatService));
        }

        public async Task JoinGroupAsync(int chatId, int userId)
        {
            var existingUserChatViewDto = await _userChatService.GetByUserIdAndChatIdAsync(chatId, userId)
                ?? throw new AccessDeniedException("User is not a chat member.");

            await Groups.AddToGroupAsync(Context.ConnectionId, existingUserChatViewDto.Id.ToString());
        }

        public async Task SendMessageAsync(int chatId, int userId, string message)
        {
            var existingUserChatViewDto = await _userChatService.GetByUserIdAndChatIdAsync(chatId, userId)
                ?? throw new AccessDeniedException("User is not a chat member.");

            await Clients.Group(existingUserChatViewDto.Id.ToString()).SendAsync("Recive", message);
        }
    }
}
