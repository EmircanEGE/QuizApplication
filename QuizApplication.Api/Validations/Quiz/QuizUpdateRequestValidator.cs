using FluentValidation;
using QuizApplication.Api.Models.Quiz;

namespace QuizApplication.Api.Validations.Quiz;

public class QuizUpdateRequestValidator : AbstractValidator<QuizUpdateRequest>
{
    public QuizUpdateRequestValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.UserId).NotEmpty();
    }
}