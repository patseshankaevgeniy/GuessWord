using GuessWord.Api.Models;
using GuessWord.BusinessLogic.Services.Interfaces;
using GuessWord.DataAccess.Repositories;
using GuessWord.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.BusinessLogic.Services
{
    public class LevelsService : ILevelsService
    {
        private readonly IWordsRepository _wordsRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly Random _randomizer;

        public LevelsService(
            IWordsRepository wordsRepository,
            ICurrentUserService currentUserService)
        {
            _wordsRepository = wordsRepository;
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
            var targetWords = _wordsRepository.GetWordsWithTranslation(userId, WordStatus.InProgress);
            var words = await _wordsRepository.GetOptionsWordsAsync();
            List<int> targetWordsIndex = new List<int> { -1 };

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
                    Target = targetWords[randomIndex].Value,
                    Options = new List<OptionDto>()
                };
                var correctOptionIndex = _randomizer.Next(0, optionsCount - 1);
                for (int p = 0; p < optionsCount; p++)
                {
                    var option = new OptionDto();
                    if (p == correctOptionIndex)
                    {
                        option.Word = targetWords[randomIndex].Translation;
                        option.IsCorrect = true;
                    }
                    else
                    {
                        option.Word = words[_randomizer.Next(words.Count - 1)].Value;
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
