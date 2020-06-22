using FluentValidation;
using LeoQuiz.Core.Dto;

namespace LeoQuiz.Core.Validators
{
    public class QuizValidator : AbstractValidator<QuizDto>
    {
        public QuizValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Mane is empty");
            RuleFor(x => x.MaxAttempts)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Max attempts is less than 1");
            RuleFor(x => x.PassGrade)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Max attempts is less than 1");
        }
    }
}
