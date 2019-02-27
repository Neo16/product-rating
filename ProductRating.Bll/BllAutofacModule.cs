using Autofac;
using ProductRating.Bll.Services;

namespace ProductRating.Bll
{
    public class BllAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)                 
                  .Where(t => t.IsSubclassOf(typeof(ServiceBase)))
                  .AsImplementedInterfaces();
        }
    }
}
