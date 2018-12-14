using System;

namespace CarRentalConsoleClient
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string message) : base(message)
        {

        }
    }
}
