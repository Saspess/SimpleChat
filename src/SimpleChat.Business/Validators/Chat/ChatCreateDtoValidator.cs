using FluentValidation;
using SimpleChat.Business.DTOs.Chat;

namespace SimpleChat.Business.Validators.Chat
{
    public class ChatCreateDtoValidator : AbstractValidator<ChatCreateDto>
    {
        public ChatCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(c => c.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(250);
        }
    }
}
