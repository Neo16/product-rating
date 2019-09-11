using Autofac;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll;
using ProductRating.Dal;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Frameworks.Autofac;

[assembly: TestFramework("ProductRating.UnitTest.ConfigureTestFramework", "ProductRating.UnitTest")]
namespace ProductRating.UnitTest
{
    public class ConfigureTestFramework : AutofacTestFramework
    {
        public ConfigureTestFramework(IMessageSink diagnosticMessageSink)
            : base(diagnosticMessageSink)
        {
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {

            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                 .Options;

            builder
                .RegisterType<ApplicationDbContext>()
                .WithParameter("options", dbOptions)
                .InstancePerLifetimeScope();

            builder.RegisterModule<BllAutofacModule>();
        }
    }
}
