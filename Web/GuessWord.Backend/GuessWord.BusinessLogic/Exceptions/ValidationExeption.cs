using System;

namespace GuessWord.BusinessLogic.Exceptions
{
    public class ValidationExeption : Exception
    {
        public ValidationExeption(string message) : base(message)
        {

        }
    }
}
