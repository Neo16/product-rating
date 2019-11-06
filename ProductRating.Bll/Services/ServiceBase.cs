using ProductRating.Dal;

namespace ProductRating.Bll.Services
{
    public abstract class ServiceBase
    {
        protected readonly ApplicationDbContext context;

        public ServiceBase(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
