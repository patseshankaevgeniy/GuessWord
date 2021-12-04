using System.ComponentModel;

namespace GuessWord.Mobile.Infrastructure
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
