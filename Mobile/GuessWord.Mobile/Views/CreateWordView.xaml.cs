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
           
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            
            base.OnDisappearing();
        }

        private void OpenSearchBar(object sender, TextChangedEventArgs args)
        {
            ViewModel.Validate();
        }
    }
}