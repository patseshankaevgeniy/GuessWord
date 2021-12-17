using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace GuessWord.Mobile.Infrastructure
{
    public class BaseView<TViewModel> : ContentPage where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel { get; set; }

        public BaseView()
        {
            var viewModel = App.ServiceProvider.GetService<TViewModel>();
            BindingContext = viewModel;
            ViewModel = viewModel;

        }
    }
}