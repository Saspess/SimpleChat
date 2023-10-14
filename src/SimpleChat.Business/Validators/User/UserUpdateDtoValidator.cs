using FluentValidation;
using SimpleChat.Business.DTOs.User;

namespace SimpleChat.Business.Validators.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(u => u.Id)
                .NotNull();

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(u => u.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .MaximumLength(100);
        }
    }
}
