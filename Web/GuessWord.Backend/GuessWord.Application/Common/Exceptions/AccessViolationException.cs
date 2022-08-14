using System;

namespace GuessWord.Application.Exceptions
{
    public class AccessViolationException : Exception
    {
        public AccessViolationException(string message) : base(message)
        {

        }
    }
}
