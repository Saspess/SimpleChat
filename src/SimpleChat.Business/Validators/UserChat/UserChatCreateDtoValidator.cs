using FluentValidation;
using SimpleChat.Business.DTOs.UserChat;

namespace SimpleChat.Business.Validators.UserChat
{
    public class UserChatCreateDtoValidator : AbstractValidator<UserChatCreateDto>
    {
        public UserChatCreateDtoValidator()
        {
            RuleFor(u => u.UserId)
                .NotNull();

            RuleFor(u => u.ChatId)
                .NotNull();
        }
    }
}
