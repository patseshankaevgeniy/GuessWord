using GuessWord.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWordView : ContentPage
    {
        public AddWordView()
        {
            InitializeComponent();
            BindingContext = new AddWordViewModel();

        }
    }
}