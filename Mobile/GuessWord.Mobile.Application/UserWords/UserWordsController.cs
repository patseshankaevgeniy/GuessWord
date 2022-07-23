using System;

namespace GuessWord.Mobile.Application.UserWords
{
    public class UserWordsController
    {
        public event Action<string> UserWordAdded;

        public void RiseUserWordAdded(string word)
        {
            UserWordAdded?.Invoke(word);
        }
    }
}
