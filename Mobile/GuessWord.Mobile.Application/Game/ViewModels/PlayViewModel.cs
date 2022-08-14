﻿using GuessWord.Mobile.Application.Common;
using GuessWord.Mobile.Application.Common.Interfaces;
using GuessWord.Mobile.Application.Game.Controllers;
using GuessWord.Mobile.Application.Game.Models;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Application.Game.ViewModels
{
    public class PlayViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILevelsController _levelsController;

        private int _currentStep;
        private Level _currentLevel;
        private int _levelNumber;

        public string Step { get; set; }
        public string TargetWord { get; set; }
        public string FirstOption { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }

        public Command SelectFirstOptionCommand { get; set; }
        public Command SelectSecondOptionCommand { get; set; }
        public Command SelectThirdOptionCommand { get; set; }
        public Command GoToPreviosCommand { get; set; }
        public Command GoToNextCommand { get; set; }
        public Command FinishCommand { get; set; }


        public PlayViewModel(
            INavigationService navigationService,
            ILevelsController levelsController)
        {
            _navigationService = navigationService;
            _levelsController = levelsController;
            FinishCommand = new Command(NavigateToFinishGame);
            GoToNextCommand = new Command(ChangeToNextLevel);
            GoToPreviosCommand = new Command(ChangeToPreviosCommand);
        }


        public override async Task OnInitializedAsync()
        {
            var level = await _levelsController.GetLevelAsync();

            var levelMobile = new Level
            {
                Count = level.Count,

                Steps = level.Steps.Select(x => new Step
                {
                    Target = x.Target,
                    Options = x.Options.Select(x => new Option
                    {
                        IsCorrect = x.IsCorrect,
                        OrderNumber = x.OrderNumber,
                        Word = x.Word
                    }).ToList(),
                }).ToList()
            };




            if (level != null)
            {
                _currentLevel = levelMobile;
                _currentStep = 0;
                _levelNumber = 1;
                var step = levelMobile.Steps[0];


                Step = $"{_levelNumber}/{_currentLevel.Count}";
                TargetWord = step.Target;
                FirstOption = step.Options[0].Word;
                SecondOption = step.Options[1].Word;
                ThirdOption = step.Options[2].Word;
            }
        }

        private void ChangeToNextLevel()
        {
            if (_levelNumber < _currentLevel.Count && _currentStep < _currentLevel.Count - 1)
            {
                _levelNumber++;
                _currentStep++;
            }

            Step = $"{_levelNumber}/{_currentLevel.Count}";



            var step = _currentLevel.Steps[_currentStep];

            TargetWord = step.Target;
            FirstOption = step.Options[0].Word;
            SecondOption = step.Options[1].Word;
            ThirdOption = step.Options[2].Word;
        }

        private void ChangeToPreviosCommand()
        {
            if (_levelNumber > 0 && _currentStep > 0)
            {
                _levelNumber--;
                _currentStep--;
            }

            Step = $"{_levelNumber}/{_currentLevel.Count}";

            var step = _currentLevel.Steps[_currentStep];
            TargetWord = step.Target;
            FirstOption = step.Options[0].Word;
            SecondOption = step.Options[1].Word;
            ThirdOption = step.Options[2].Word;
        }

        public async void NavigateToFinishGame()
        {
            await _navigationService.NavigateToFinishGameAsync();
        }
    }
}