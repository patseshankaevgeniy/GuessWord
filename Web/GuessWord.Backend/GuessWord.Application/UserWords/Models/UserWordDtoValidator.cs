using FluentValidation;

namespace GuessWord.Application.UserWords.Models
{
    public class UserWordDtoValidator : AbstractValidator<UserWordDto>
    {
        public UserWordDtoValidator()
        {
            RuleFor(x => x.Word).NotEmpty();
            RuleFor(x => x.Language).GreaterThan(-1).LessThan(2);
            RuleFor(x => x.Translations).NotEmpty();
        }
    }
}
