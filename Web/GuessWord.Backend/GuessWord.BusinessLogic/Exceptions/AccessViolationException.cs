using System;

namespace GuessWord.BusinessLogic.Exceptions
{
    public class AccessViolationException : Exception
    {
        public AccessViolationException(string message) : base(message)
        {

        }
    }
}
