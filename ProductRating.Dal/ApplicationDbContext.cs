using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductRating.Dal.Model.Entities;
using ProductRating.Dal.Model.Entities.Products;
using ProductRating.Dal.Model.Entities.Products.Attributes;
using ProductRating.Dal.Model.Entities.Reviews;
using ProductRating.Dal.Model.Identity;
using System;
using System.Linq;
using System.Reflection;

namespace ProductRating.Dal
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }

        public virtual DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

        public virtual DbSet<ProductAttributeValueConnection> ProductAttributeValueConnections { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Scorereview> Scores { get; set; }

        public virtual DbSet<TextReview> Reviews { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<ReviewVote> ReviewVotes { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductAttributeInt>();
            builder.Entity<ProductAttributeString>();

            builder.Entity<ProductAttributeIntValue>();
            builder.Entity<ProductAttributeStringValue>();


            #region Identity_Table_Names
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            #endregion

            #region register IEntityTypeConfigurations
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                         .Where(t => t.GetInterfaces()
                         .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                         .ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }
            #endregion

        }
    }
}
