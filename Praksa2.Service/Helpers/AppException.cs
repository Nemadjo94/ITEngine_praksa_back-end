﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Praksa2.Service
{
    /// <summary>
    /// Custom exceptions class used to differentiate between handled and unhandled exceptions.
    /// Handled exceptions are ones generated by the application and used to display friendly
    /// error messages to the client
    /// </summary>
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
