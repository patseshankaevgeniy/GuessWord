using FluentValidation;

namespace GuessWord.Application.Words.Models
{
    public class WordDtoValidator : AbstractValidator<WordDto>
    {
        public WordDtoValidator()
        {
            var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

            RuleFor(x => x.Value)
                .NotNull().WithMessage(msg)
                .NotEmpty().WithMessage(msg);

            RuleFor(x => x.Translations)
                .NotEmpty().WithMessage(msg);
        }
    }
}
