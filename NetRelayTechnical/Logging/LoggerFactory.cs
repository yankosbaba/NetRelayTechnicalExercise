using log4net;
using log4net.Config;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace NetRelayTechnical.Logging
{
    public class LoggerFactory
    {
        public static ILogger CreateLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            ILogger logger = DependencyConfiguration.Kernel.Get<Log4NetLoggingAdapter>();
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            return logger;
        }
    }
}

