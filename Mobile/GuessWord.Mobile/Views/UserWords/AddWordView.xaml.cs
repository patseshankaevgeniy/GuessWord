using GuessWord.Mobile.Application.UserWords.ViewModels;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.UserWords
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