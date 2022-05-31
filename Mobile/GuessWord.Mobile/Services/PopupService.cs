using GuessWord.Mobile.Application.Common.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuessWord.Mobile.Services
{
    public class PopupService : IPopupService
    {
        public async Task<PopupResult<string>> ShowAddWordTranslationPopup()
        {
            var result = await Shell.Current.DisplayPromptAsync(
                title: "Create Translation",
                message: "Please enter new translation",
                accept: "Save",
                cancel: "Cancel",
                placeholder: "Перевод",
                maxLength: 10);

            if (!string.IsNullOrEmpty(result))
            {
                return new PopupResult<string> { Result = result, Succeeded = true };
            }
            else
            {
                return new PopupResult<string> { Succeeded = false };
            }

        }

        public async Task<PopupResult<string>> ShowChangeStatusPopup()
        {
            string result = await Shell.Current.DisplayActionSheet(
               title: "Choose status:",
               cancel: null,
               destruction: "Cancel",
               "In Progress",
               "Pause",
               "Completed") ;

            if (!string.IsNullOrEmpty(result))
            {
                return new PopupResult<string> { Result = result, Succeeded = true };
            }
            else
            {
                return new PopupResult<string> { Succeeded = false };
            }
        }

        public async Task<PopupResult<string>> ShowEditWordTranslationPopup(string initialValue)
        {
            var result = await Shell.Current.DisplayPromptAsync(
                title: "Edit Translation",
                message: "Please enter new translation",
                accept: "Save",
                cancel: "Cancel",
                placeholder: "Перевод",
                maxLength: 10,
                initialValue: initialValue);

            if (result != null && result != initialValue)
            {
                return new PopupResult<string> { Result = result, Succeeded = true };
            }
            else
            {
                return new PopupResult<string> { Succeeded = false };
            }
        }

        public async Task<PopupResult<bool>> ShowRemoveWordTranslationPopup()
        {
            bool result = await Shell.Current.DisplayAlert(
                title:"Delete the word?",
                message:"",
                accept:"Delete",
                cancel:"Cancel");

            if (result)
            {
                return new PopupResult<bool> { Result = true };
            }
            else
            {
                return new PopupResult<bool> { Result= false };
            }
        }

        public async Task<PopupResult<bool>> ShowSaveConfirmationPopupAsync()
        {
            bool result = await Shell.Current.DisplayAlert(
               title: "Save the word?",
               message: "",
               accept: "Save",
               cancel: "Cancel");

            if (result)
            {
                return new PopupResult<bool> { Succeeded = true };
            }
            else
            {
                return new PopupResult<bool> { Succeeded = false };
            }
        }
    }
}
