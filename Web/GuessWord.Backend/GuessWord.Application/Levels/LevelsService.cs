using GuessWord.Application.Common.Interfaces;
using GuessWord.Application.Common.Interfaces.Repositories;
using GuessWord.Application.Levels.Models;
using GuessWord.Domain.Entities;
using GuessWord.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessWord.Application.Levels
{
    public class LevelsService : ILevelsService
    {
        private readonly IGenericRepository<UserWord> _userWordsRepository;
        private readonly IGenericRepository<Word> _wordsRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly Random _randomizer;

        public LevelsService(
            IGenericRepository<UserWord> userWordsRepository,
            IGenericRepository<Word> wordsRepository,
            ICurrentUserService currentUserService)
        {
            _wordsRepository = wordsRepository;
            _userWordsRepository = userWordsRepository;
            _currentUserService = currentUserService;
            _randomizer = new Random();
        }

        public async Task<LevelDto> GetLevelAsync()
        {
            var level = new LevelDto
            {
                Steps = new List<StepDto>(),
            };

            var optionsCount = 3;
            var userId = _currentUserService.UserId;
            var targetWordsIndex = new List<int> { -1 };

            var targetWords = await _userWordsRepository.FindAsync(
                x => x.Status == WordStatus.InProgress,
                x => x.Include(x => x.Word).ThenInclude(x => x.Translations).ThenInclude(x => x.Translation));

            var optionWords = await _wordsRepository.FindAsync(
                x => x.Language == Language.Russian);

            for (int i = 0; i < targetWords.Count; i++)
            {
                var randomIndex = _randomizer.Next(0, targetWords.Count - 1);
                for (int j = 0; j < targetWords.Count; j++)
                {
                    if (targetWordsIndex[j] == randomIndex)
                    {
                        if (randomIndex == targetWords.Count - 1)
                        {
                            randomIndex = 0;
                        }
                        else
                        {
                            randomIndex++;
                            j = 0;
                        }
                    }
                    if (j == targetWordsIndex.Count - 1)
                    {
                        targetWordsIndex.Add(randomIndex);
                        break;
                    }
                }

                var step = new StepDto
                {
                    Target = targetWords[randomIndex].Word.Value,
                    Options = new List<OptionDto>()
                };

                var correctOptionIndex = _randomizer.Next(0, optionsCount - 1);
                for (int p = 0; p < optionsCount; p++)
                {
                    var option = new OptionDto();
                    if (p == correctOptionIndex)
                    {
                        option.Word = string.Join(",", targetWords[randomIndex].Word.Translations.Select(x => x.Translation)); ;
                        option.IsCorrect = true;
                    }
                    else
                    {
                        option.Word = optionWords[_randomizer.Next(optionWords.Count - 1)].Value;
                        option.IsCorrect = false;
                    }
                    option.OrderNumber = p;
                    step.Options.Add(option);
                }
                level.Steps.Add(step);
            }
            level.Count = targetWords.Count;
            return level;
        }
    }
}
