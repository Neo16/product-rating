using ProductRating.Dal;
using System;
using System.Collections.Generic;
using System.Text;

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
