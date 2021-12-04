using GuessWord.Mobile.Infrastructure;
using Xamarin.Forms;

namespace GuessWord.Mobile.ViewModels
{
    public class AddWordViewModel : BaseViewModel
    {
        public string Word { get; set; }
        public string NewWord { get; set; }
        public Command ChangeWordCommand { get; set; }

        public AddWordViewModel()
        {
            ChangeWordCommand = new Command(ChangeWord);
            Word = "Bob";
        }

        private void ChangeWord()
        {
            Word = "Home";
        }
    }
}
