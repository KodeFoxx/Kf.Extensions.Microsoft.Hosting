using System;

namespace Kf.Extensions.Microsoft.Hosting.Console
{
    public sealed class ConsoleHostBuilderException : Exception
    {
        public ConsoleHostBuilderException(
            string message)
            : base(message)
        { }

        public ConsoleHostBuilderException(
            string message,
            Exception innerException)
            : base(message, innerException)
        { }
    }
}
