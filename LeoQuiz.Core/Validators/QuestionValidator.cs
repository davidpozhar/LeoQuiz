using FluentValidation;
using LeoQuiz.Core.Dto;

namespace LeoQuiz.Core.Validators
{
    public class QuestionValidator : AbstractValidator<QuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Text of question is empty");
        }
    }
}
