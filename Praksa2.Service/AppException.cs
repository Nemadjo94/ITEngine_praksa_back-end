using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Praksa2.Service
{
    /// <summary>
    /// Custom exception class for throwing application specific exceptions
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
