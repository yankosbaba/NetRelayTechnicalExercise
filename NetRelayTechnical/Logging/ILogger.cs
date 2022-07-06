using System;
using System.Collections.Generic;
using System.Text;

namespace NetRelayTechnical.Logging
{
    public interface ILogger
    {
        bool IsDebugEnabled { get; }
        bool IsVerboseEnabled { get; }
        bool IsInformationalEnabled { get; }
        bool IsWarningEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }

        void Write(string message, EventSeverity severity);
        void Write(string message, Exception exception, EventSeverity severity);
    }
}
