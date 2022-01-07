using System.ComponentModel;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Infrastructure
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
