using AutoMapper;
using GuessWord.Domain.Entities;

namespace GuessWord.Application.Words.Models
{
    public class WordAutoMapper : Profile
    {
        public WordAutoMapper()
        {
            CreateMap<Word, WordDto>().ReverseMap();
        }
    }
}
