using GuessWord.Mobile.Application.Game.ViewModels;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.Game
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishGameView : BaseView<FinishGameViewModel>
    {
        public FinishGameView()
        {
            InitializeComponent();
        }
    }
}