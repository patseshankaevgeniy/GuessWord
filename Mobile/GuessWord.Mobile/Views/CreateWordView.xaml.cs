using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateWordView : BaseView<CreateWordViewModel>
    {
        public CreateWordView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            WordValueEntry.TextChanged += EntryOnTextChanged;
            TranslationEntry.TextChanged += EntryOnTextChanged;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            WordValueEntry.TextChanged -= EntryOnTextChanged;
            TranslationEntry.TextChanged -= EntryOnTextChanged;
            base.OnDisappearing();
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs args)
        {
            ViewModel.Validate();
        }
    }
}