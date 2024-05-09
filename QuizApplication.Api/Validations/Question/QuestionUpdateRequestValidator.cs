using FluentValidation;
using QuizApplication.Api.Models.Question;

namespace QuizApplication.Api.Validations.Question;

public class QuestionUpdateRequestValidator : AbstractValidator<QuestionUpdateRequest>
{
    public QuestionUpdateRequestValidator()
    {
        RuleFor(x => x.Text).NotEmpty().MaximumLength(500);
        RuleFor(x => x.QuizId).NotEmpty();
    }
}