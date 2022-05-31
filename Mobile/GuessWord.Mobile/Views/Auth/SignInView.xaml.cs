using GuessWord.Mobile.Application.Auth.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.Auth
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