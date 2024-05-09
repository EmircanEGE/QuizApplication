using FluentValidation;
using QuizApplication.Api.Models.User;

namespace QuizApplication.Api.Validations.User;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(16);
    }
}