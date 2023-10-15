using FluentValidation;
using SimpleChat.Business.DTOs.Chat;

namespace SimpleChat.Business.Validators.Chat
{
    public class ChatDeleteDtoValidator : AbstractValidator<ChatDeleteDto>
    {
        public ChatDeleteDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotNull();

            RuleFor(c => c.CreatorId)
                .NotNull();
        }
    }
}
