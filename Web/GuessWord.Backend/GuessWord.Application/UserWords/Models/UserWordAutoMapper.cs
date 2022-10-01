using AutoMapper;
using GuessWord.Domain.Entities;
using System.Linq;

namespace GuessWord.Application.UserWords.Models
{
    public class UserWordAutoMapper : Profile
    {
        public UserWordAutoMapper()
        {
            CreateMap<UserWord, UserWordDto>()
                .ForMember(x => x.Word, o => o.MapFrom(u => u.Word.Value))
                .ForMember(x => x.Translations, o => o.MapFrom(u => u.Word.Translations.Select(t => t.Translation.Value)))
                .ReverseMap();
        }
    }
}
