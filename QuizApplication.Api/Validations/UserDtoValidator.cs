using FluentValidation;
using QuizApplication.Application.Dtos;

namespace QuizApplication.Api.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(3);
        }

    }
}
