using Microsoft.AspNetCore.Identity;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
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
                new Category()
                {
                    Name = "Furniture",
                    Attributes = new List<ProductAttribute>()
                    {
                        new ProductAttributeString() {Name = "Material" },
                        new ProductAttributeInt () {Name = "Width" },
                        new ProductAttributeInt () {Name = "Height" },
                        new ProductAttributeInt () {Name = "Depth" }
                    }
                },
                new Category()
                {
                    Name = "Consumer electronics",
                    Children = new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Laptops",
                            Attributes = new List<ProductAttribute>()
                            {
                                new ProductAttributeString() {Name = "Processor" }                               
                            }
                        }
                    }
                },
                new Category()
                {
                    Name = "Books",
                    Attributes = new List<ProductAttribute>()
                    {
                        new ProductAttributeString() {Name = "Author" }
                    }
                },
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateBrands(this ApplicationDbContext context)
        {
            var brands = new List<Brand>()
            {
                new Brand(){ Name = "Lenovo" }                
            };

            context.Brands.AddRange(brands);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateProducts(this ApplicationDbContext context)
        {
            var computer1 = new Product()
            {
                BrandId = context.Brands.Where(e => e.Name == "Lenovo").First().Id,
                CategoryId = context.Categories.Where(e => e.Name == "Laptops").First().Id,
                Name = "IdeaPad Z50-75",
                PropertyValues = new List<ProductAttributeValue>()
                {
                    new ProductAttributeStringValue()
                    {
                        StringValue = "Intel i7-4510U",
                        Attribute = context.ProductAttribute.Where(e => e.Name == "Processor").First()
                    }
                }
            };
            context.Add(computer1);
            context.SaveChanges();
            return context;
        }
    }
}
