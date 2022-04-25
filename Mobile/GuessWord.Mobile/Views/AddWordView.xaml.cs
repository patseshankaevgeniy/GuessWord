using GuessWord.Mobile.Infrastructure;
using GuessWord.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWordView : BaseView<AddWordViewModel>
    {
        public AddWordView()
        {
            InitializeComponent();
        }
    }
}