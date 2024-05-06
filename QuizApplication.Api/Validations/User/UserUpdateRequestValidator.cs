using FluentValidation;
using QuizApplication.Api.Models.User;

namespace QuizApplication.Api.Validations.User
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(16);
        }
    }
}
