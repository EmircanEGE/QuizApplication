using FluentValidation;
using QuizApplication.Api.Models.Answer;

namespace QuizApplication.Api.Validations.Answer
{
    public class AnswerUpdateRequestValidator : AbstractValidator<AnswerUpdateRequest>
    {
        public AnswerUpdateRequestValidator()
        {
            RuleFor(x => x.Text).NotEmpty().MaximumLength(500);
            RuleFor(x => x.QuestionId).NotEmpty();
            RuleFor(x => x.IsCorrect).NotEmpty();
        }
    }
}
