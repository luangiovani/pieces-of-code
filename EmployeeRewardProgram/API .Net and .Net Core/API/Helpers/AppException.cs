using System;
using System.Globalization;

namespace ERwPHelpers
{

    // Exception customizada para tratamento de exceções específicas como validação, casting...
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
