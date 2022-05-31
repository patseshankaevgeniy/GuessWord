using System.ComponentModel;
using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Common
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual Task OnInitializedAsync()
        {
            return Task.CompletedTask;
        }
        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
