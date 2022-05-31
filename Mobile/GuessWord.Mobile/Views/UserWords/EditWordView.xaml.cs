using GuessWord.Mobile.Application.UserWords.ViewModels;
using System;
using Xamarin.Forms.Xaml;

namespace GuessWord.Mobile.Views.UserWords
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWordView : BaseView<EditWordViewModel>
    {
        public EditWordView()
        {
            InitializeComponent();
        }

        
    }
}