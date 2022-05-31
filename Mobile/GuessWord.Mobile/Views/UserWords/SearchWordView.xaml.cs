using GuessWord.Mobile.Application.UserWords.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.UserWords
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchWordView : BaseView<SearchWordViewModel>
    {
        public SearchWordView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            WordSearchBar.TextChanged += OntextChange;
            base.OnAppearing();
        }


        protected override void OnDisappearing()
        {

            base.OnDisappearing();
        }

        private async void OntextChange(object sender, TextChangedEventArgs args)
        {
            await ViewModel.SearchWordsAsync(args.NewTextValue);

        }

        private void OpenSearchBar(object sender, TextChangedEventArgs args)
        {
            // ViewModel.Validate();
        }
    }
}