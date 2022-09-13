using FluentValidation;

namespace GuessWord.Application.UserWords.Models
{
    public class UserWordDtoValidator : AbstractValidator<UserWordDto>
    {
        public UserWordDtoValidator()
        {
            var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

            RuleFor(x => x.Word)
                .NotNull().WithMessage(msg)
                .NotEmpty().WithMessage(msg);

            RuleFor(x => x.Language)
                .GreaterThan(-1).WithMessage(msg)
                .LessThan(2).WithMessage(msg);

            RuleFor(x => x.Translations)
                .NotEmpty().WithMessage(msg);
        }
    }
}
