using log4net;
using Ninject;
using Ninject.Modules;

namespace NetRelayTechnical.Logging
{
    public class DependencyConfiguration : NinjectModule
    {
        public new static IKernel Kernel = new StandardKernel(new DependencyConfiguration());
        public override void Load()
        {
            Bind<ILog>().ToMethod(x => LogManager.GetLogger(x.Request.Target?.Member.DeclaringType));
            Bind<ILogger>().To<Log4NetLoggingAdapter>();
        }
    }
}
