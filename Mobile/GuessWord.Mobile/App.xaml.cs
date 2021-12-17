﻿using GuessWord.Mobile.Views;
using Xamarin.Forms;

namespace GuessWord.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AddWordView();
            MainPage = new SignInView();
            // MainPage = new NavigationPage(new SignIn());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}