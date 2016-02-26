using System;

namespace NPlant.Console.Exceptions
{
    public class ConsoleUsageException : Exception
    {
        public ConsoleUsageException(string message) : base(message)
        {
        }

        public ConsoleUsageException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}