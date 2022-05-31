using GuessWord.Mobile.Application.Auth.ViewModels;
using GuessWord.Mobile.Infrastructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.Auth
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : BaseView<SignUpViewModel>
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            UserNameEntry.TextChanged += EntryOnTextChanged;
            LoginEntry.TextChanged += EntryOnTextChanged;
            PasswordEntry.TextChanged += EntryOnTextChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            UserNameEntry.TextChanged -= EntryOnTextChanged;
            LoginEntry.TextChanged -= EntryOnTextChanged;
            PasswordEntry.TextChanged -= EntryOnTextChanged;
            base.OnDisappearing();
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs args)
        {
            ViewModel.Validate();
        }
    }
}