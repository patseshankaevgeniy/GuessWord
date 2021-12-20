using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpView : BaseView<SignUpViewModel>
    {
        public SignUpView()
        {
            InitializeComponent();
            //BindingContext = App.ServiceProvider.GetService<SignUpViewModel>();
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