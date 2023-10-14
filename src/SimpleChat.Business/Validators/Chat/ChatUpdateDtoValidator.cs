using FluentValidation;
using SimpleChat.Business.DTOs.Chat;

namespace SimpleChat.Business.Validators.Chat
{
    public class ChatUpdateDtoValidator : AbstractValidator<ChatUpdateDto>
    {
        public ChatUpdateDtoValidator()
        {
            RuleFor(c => c.Id)
                .NotNull();

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
