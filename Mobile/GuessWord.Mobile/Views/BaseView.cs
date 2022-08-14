﻿using GuessWord.Mobile.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace GuessWord.Mobile.Views
{
    public class BaseView<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        private bool _initialized = false;

        public TViewModel ViewModel { get; set; }

        public BaseView()
        {
            var viewModel = App.ServiceProvider.GetService<TViewModel>();
            BindingContext = viewModel;
            ViewModel = viewModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!_initialized)
            {
                await ViewModel.OnInitializedAsync();
                _initialized = true;
            }

            await ViewModel.OnAppearingAsync();
        }
    }
}