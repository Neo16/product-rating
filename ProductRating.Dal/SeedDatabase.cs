using Microsoft.AspNetCore.Identity;
using ProductRating.Common;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using ProductRating.Model.Entities.Reviews;
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
                    .CreateRoles()
                    .CreateUsers()
                    .CreateCategories()
                    .CreateBrands()
                    .CreateProducts()
                    .CreateReviews(20);
            }
        }

        private static ApplicationDbContext CreateRoles(this ApplicationDbContext context)
        {
            var r = new Random();
            var adminRole = new ApplicationRole()
            {
                Name = RoleNames.ADMIN_ROLE,
                NormalizedName = RoleNames.ADMIN_ROLE,
                ConcurrencyStamp = r.Next().ToString(),                
            };

            var userRole = new ApplicationRole()
            {
                Name = RoleNames.USER_ROLE,
                NormalizedName = RoleNames.USER_ROLE,
                ConcurrencyStamp = r.Next().ToString(),
            };

            var ownerRole = new ApplicationRole()
            {
                Name = RoleNames.SHOP_OWNER_ROLE,
                NormalizedName = RoleNames.SHOP_OWNER_ROLE,
                ConcurrencyStamp = r.Next().ToString(),
            };

            context.Roles.Add(userRole);
            context.Roles.Add(adminRole);
            context.Roles.Add(ownerRole);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateUsers(this ApplicationDbContext context)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser()
            {
                Email = "user@productrating.com",
                NormalizedEmail = "USER@PRODUCTRATING.COM",
                UserName = "user@productrating.com",
                NormalizedUserName = "USER@PRODUCTRATING.COM",
                EmailConfirmed = true,
                SecurityStamp = "3543545345",
                PhoneNumber = "+311124211",
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            
            context.Users.Add(user);
            context.SaveChanges();

            context.UserRoles.Add(new IdentityUserRole<Guid>() {
                RoleId = context.Roles.Where(e => e.Name == RoleNames.USER_ROLE).Single().Id,
                UserId = user.Id
            });

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

        private static ApplicationDbContext CreateReviews(this ApplicationDbContext context, int numberOfReviews)
        {
            var users = context.Users.ToList();
            var products = context.Products.ToList();
            var r = new Random();            

            var reviews = Enumerable.Range(1, numberOfReviews)
               .Select(e => new TextReview()
               {
                   AuthorId = users[r.Next(0,users.Count)].Id,
                   ProductId = products[r.Next(0, products.Count)].Id,
                   Mood = (ReviewMood)r.Next(1,2),
                   CreatedAt = DateTime.Now.AddSeconds(1 - r.Next(50000)),
                   Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                   "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                   " Eu turpis egestas pretium aenean pharetra magna ac placerat."                   
               })
               .ToList();

            context.Reviews.AddRange(reviews);
            context.SaveChanges();

            return context;   
        }
    }
}
