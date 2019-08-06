using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Service
{
    /// <summary>
    /// Contains properties defined in the appsettings.json file and
    /// is used for accessing application settings via objects that injected
    /// into classes using the ASP.NET Core built in dependency injection.
    /// </summary>
    public class AppSettings
    {
        public string Secret { get; set; }
    }
}
