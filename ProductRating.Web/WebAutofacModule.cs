using Autofac;
using ProductRating.Web.WebServices;

namespace ProductRating.Web
{
    public class WebAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrentUserService>()
                  .AsSelf();
        }
    }
}
