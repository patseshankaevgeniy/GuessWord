using GuessWord.Mobile.Application.Home;
using GuessWord.Mobile.Infrastructure;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : BaseView<HomeViewModel>
    {
        public HomeView()
        {
            InitializeComponent();
        }
    }
}