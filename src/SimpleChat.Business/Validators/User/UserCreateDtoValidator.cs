using FluentValidation;
using SimpleChat.Business.DTOs.User;

namespace SimpleChat.Business.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
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
