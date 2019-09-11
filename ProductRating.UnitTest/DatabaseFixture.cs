using ProductRating.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductRating.UnitTest
{
    public class DatabaseFixture : IDisposable
    {
        protected readonly ApplicationDbContext context;

        public DatabaseFixture(ApplicationDbContext context)
        {
            this.context = context;           
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
