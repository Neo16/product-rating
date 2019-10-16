using Autofac;
using log4net;
using Microsoft.AspNetCore.Http;
using ProductRating.Web.WebServices;
using System.IO;
using System.Xml;

namespace ProductRating.Web
{
    public class WebAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            XmlDocument log4netConfig = new XmlDocument();

            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(
                System.Reflection.Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy)
            );
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            ILog logger = LogManager.GetLogger(typeof(Program));
            builder.RegisterInstance(logger).As<ILog>().SingleInstance();

            builder.RegisterType<CurrentUserService>()
                      .AsSelf();
            builder.RegisterType<HttpContextAccessor>()
                  .As<IHttpContextAccessor>();
        }
    }
}
