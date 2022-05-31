using System.Threading.Tasks;

namespace GuessWord.Mobile.Application.Common.Interfaces
{
    public class PopupResult<T>
    {
        public bool Succeeded { get; set; }
        public T Result { get; set; }
    }

    public interface IPopupService
    {
        // Async добавить
        Task<PopupResult<string>> ShowEditWordTranslationPopup(string initialValue);
        Task<PopupResult<string>> ShowAddWordTranslationPopup();
        Task<PopupResult<bool>> ShowSaveConfirmationPopupAsync();
        Task<PopupResult<bool>> ShowRemoveWordTranslationPopup();
        Task<PopupResult<string>> ShowChangeStatusPopup();
    }
}
