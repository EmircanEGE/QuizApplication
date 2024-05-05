using FluentValidation;
using QuizApplication.Api.Models.User;
using QuizApplication.Application.Dtos;

namespace QuizApplication.Api.Validations
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(16);
        }

    }
}
