using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.Services;
using GuessWord.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInView : BaseView<SignInViewModel>
    {
        public SignInView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoginEntry.TextChanged += EntryOnTextChanged;
            PasswordEntry.TextChanged += EntryOnTextChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
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