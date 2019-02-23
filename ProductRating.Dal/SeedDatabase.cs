using Microsoft.AspNetCore.Identity;
using ProductRating.Model.Entities.Product;
using ProductRating.Model.Entities.Product.Attributes;
using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductRating.Dal
{
    public static class SeedDatabase
    {
        public static void Seed(this ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context
                    .CreateUsers()
                    .CreateCategories()
                    .CreateBrands()
                    .CreateProducts();
            }
        }

        private static ApplicationDbContext CreateUsers(this ApplicationDbContext context)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser()
            {
                Email = "user@productrating.com",
                NormalizedEmail = "USER@PRODUCTRATING.COM",
                UserName = "user@planny.com",
                NormalizedUserName = "USER@PRODUCTRATING.COM",
                EmailConfirmed = true,
                SecurityStamp = "3543545345",
                PhoneNumber = "+311124211",
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            context.Users.Add(user);
            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateCategories(this ApplicationDbContext context)
        {
            var categories = new List<Category>()
            {
                new Category(){ Name = "Bútorok" },
                new Category(){ Name = "Szórakoztató elektronika" },
                new Category(){ Name = "Könyvek"},
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateBrands(this ApplicationDbContext context)
        {
            var brands = new List<Brand>()
            {
                new Brand(){ Name = "Tesco" }                
            };

            context.Brands.AddRange(brands);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateProducts(this ApplicationDbContext context)
        {
            var product = new Product()
            {
                BrandId = context.Brands.First().Id,
                CategoryId = context.Categories.First().Id,
                Name = "Egy izé",
                PropertyValues = new List<ProductAttributeValue>()
                {
                    new ProductAttributeStringValue()
                    {
                        StringValue = "Ez az érték",
                        ProductAttribute = new ProductAttribute()
                        {
                            Name = "asd",
                            CategoryId = context.Categories.First().Id
                        }
                    }
                }


            };
            context.Add(product);
            context.SaveChanges();
            return context;
        }
    }
}
