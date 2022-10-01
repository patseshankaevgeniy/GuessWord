using FluentValidation;

namespace GuessWord.Application.Words.Models
{
    public class WordDtoValidator : AbstractValidator<WordDto>
    {
        public WordDtoValidator()
        {
            RuleFor(x => x.Value).NotEmpty();
            RuleFor(x => x.Translations).NotEmpty();
        }
    }
}
