using FluentValidation;
using LeoQuiz.Core.Dto;

namespace LeoQuiz.Core.Validators
{
    public class AnswerValidator : AbstractValidator<AnswerDto>
    {
        public AnswerValidator()
        {
            RuleFor(x => x.IsCorrect)
                .NotNull()
                .WithMessage("Correct field must be true or false");

            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text is empty");

            RuleFor(x => x.QuestionId)
                .NotEmpty()
                .WithMessage("Question id is empty");
        }
    }
}
