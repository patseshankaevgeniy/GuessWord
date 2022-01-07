using GuessWord.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Services
{
    public class LevelsController : ILevelsController
    {
        public async Task<Level> GetLevelAsync()
        {
            var level = new Level();
           
            level.Steps = new List<Step>
            {
                new Step
                {
                    Target = "work",
                    Options = new List<Option>
                    {
                        new Option{ IsCorrect = true, OrderNumber = 0, Word = "Работа" },
                        new Option{ IsCorrect = false, OrderNumber = 1, Word = "Дом" },
                        new Option{ IsCorrect = false, OrderNumber = 2, Word = "Семья" },
                    } 
                },
                 new Step
                {
                    Target = "home",
                    Options = new List<Option>
                    {
                        new Option{ IsCorrect = false, OrderNumber = 0, Word = "Работа" },
                        new Option{ IsCorrect = true, OrderNumber = 1, Word = "Дом" },
                        new Option{ IsCorrect = false, OrderNumber = 2, Word = "Семья" },
                    }
                },
                  new Step
                {
                    Target = "family",
                    Options = new List<Option>
                    {
                        new Option{ IsCorrect = false, OrderNumber = 0, Word = "Работа" },
                        new Option{ IsCorrect = false, OrderNumber = 1, Word = "Дом" },
                        new Option{ IsCorrect = true, OrderNumber = 2, Word = "Семья" },
                    }
                },
            };
            level.Count = level.Steps.Count;
            await Task.Delay(3000);
            return await Task.FromResult(level);
        }
    }
}
