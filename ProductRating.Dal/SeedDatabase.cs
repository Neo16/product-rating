﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductRating.Common;
using ProductRating.Dal.Model.Entities.Products;
using ProductRating.Dal.Model.Entities.Products.Attributes;
using ProductRating.Dal.Model.Entities.Reviews;
using ProductRating.Dal.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductRating.Dal
{
    public static class SeedDatabase
    {

        static ApplicationUser webshopOwnderUser;


        public static void Seed(this ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                context
                    .CreateRoles()
                    .CreateUsers()
                    //.CreateCategories()
                    .MassCreateBrands(10)
                    .MassCreateCategories(10)
                    .MassCreateFixedValues(5) //value per fixed attr
                                              //  .CreateBrands()
                                              //  .CreateProducts()
                    .MassCreateProducts(100)
                    .CreateReviews(200);
                //CreateScrores(1);
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
            var userRoleId = context.Roles.Where(e => e.Name == RoleNames.USER_ROLE).Single().Id;
            var ownerRoleId = context.Roles.Where(e => e.Name == RoleNames.SHOP_OWNER_ROLE).Single().Id;
            var adminRoleId = context.Roles.Where(e => e.Name == RoleNames.ADMIN_ROLE).Single().Id;

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
                NickName = "Péter",
                Address = "9024, Győr, Arany János utca 11",
                Introduction = "Etiam eget neque ac nisi sodales pellentesque. Donec suscipit enim feugiat tortor ultricies faucibus. Fusce eu ante mi. Fusce iaculis sed ipsum venenatis efficitur. Praesent dolor neque, maximus sit amet cursus vel, ornare et felis. Vestibulum auctor tellus odio, vel faucibus risus tincidunt sit amet.",
                Nationality = "Hungarian",
                Subscriptions = new List<Subscription>() {
                    new Subscription() {
                         ApiKey = "123456",
                         SiteBaseUrl = "",
                         DayLimit = 200
                    }
                }

            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            context.Users.Add(user);

            var customer = new ApplicationUser()
            {
                Email = "customer@productrating.com",
                NormalizedEmail = "CUSTOMER@PRODUCTRATING.COM",
                UserName = "customer@productrating.com",
                NormalizedUserName = "CUSTOMER@PRODUCTRATING.COM",
                EmailConfirmed = true,
                SecurityStamp = "3345545345",
                PhoneNumber = "+311345211",
                NickName = "Mark Customer",
                Nationality = "USA"
            };
            customer.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            context.Users.Add(customer);

            var owner = new ApplicationUser()
            {
                Email = "webshop-owner@productrating.com",
                NormalizedEmail = "WEBSHOP-OWNER@PRODUCTRATING.COM",
                UserName = "webshop-owner@productrating.com",
                NormalizedUserName = "WEBSHOP-OWNER@PRODUCTRATING.COM",
                EmailConfirmed = true,
                SecurityStamp = "32345545345",
                PhoneNumber = "+3234345211",
                NickName = "David Owner",
                Nationality = "Denmark"
            };
            owner.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            context.Users.Add(owner);

            var admin = new ApplicationUser()
            {
                Email = "admin@productrating.com",
                NormalizedEmail = "ADMIN@PRODUCTRATING.COM",
                UserName = "admin@productrating.com",
                NormalizedUserName = "ADMIN@PRODUCTRATING.COM",
                EmailConfirmed = true,
                SecurityStamp = "3345545345",
                PhoneNumber = "+311345211",
                NickName = "Joe Admin",
                Nationality = "USA"
            };
            admin.PasswordHash = passwordHasher.HashPassword(user, "Asdf123!");
            context.Users.Add(admin);

            context.SaveChanges();
            var userRoles = new List<IdentityUserRole<Guid>>()
            {
                //"user" has all roles
                new IdentityUserRole<Guid>(){ RoleId = userRoleId, UserId = user.Id  },
                new IdentityUserRole<Guid>(){ RoleId = ownerRoleId, UserId = user.Id },
                new IdentityUserRole<Guid>(){ RoleId = adminRoleId, UserId = user.Id },
                //"customer" is only simple user
                new IdentityUserRole<Guid>(){ RoleId = userRoleId, UserId = customer.Id  },
                //"owner" is a webshop owner
                new IdentityUserRole<Guid>(){ RoleId = ownerRoleId, UserId = owner.Id  },
                //admin
                 new IdentityUserRole<Guid>(){ RoleId = adminRoleId, UserId = admin.Id  },
            };

            context.UserRoles.AddRange(userRoles);

            context.SaveChanges();
            webshopOwnderUser = owner;
            return context;
        }

        private static ApplicationDbContext CreateCategories(this ApplicationDbContext context)
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "Furniture",
                    Creator = webshopOwnderUser,
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
                            Creator = webshopOwnderUser,
                            Attributes = new List<ProductAttribute>()
                            {
                                new ProductAttributeString() {Name = "Processor", HasFixedValues = true }
                            }
                        },
                        new Category()
                        {
                            Name = "Phones",
                            Creator = webshopOwnderUser,
                            Attributes = new List<ProductAttribute>()
                            {
                                new ProductAttributeString() {Name = "Back camera" }
                            }
                        }
                    }
                },
                new Category()
                {
                    Name = "Books",
                    Attributes = new List<ProductAttribute>()
                    {
                        new ProductAttributeString() {Name = "Author" },
                        new ProductAttributeInt() { Name ="Publication date" }
                    }
                },
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
            return context;
        }


        private static ApplicationDbContext MassCreateFixedValues(this ApplicationDbContext context, int count)
        {
            var categories = context.Categories
                .Include(e => e.Attributes)
                .ToList();

            foreach (var category in categories)
            {
                for (int i = 0; i < count; i++)
                {
                    var attributeId = category.Attributes.Single(e => e.Name == "fixed string").Id;
                    var attributeValue = new ProductAttributeStringValue()
                    {
                        StringValue = "fixed " + i.ToString(),
                        AttributeId = attributeId
                    };

                    context.ProductAttributeValues.Add(attributeValue);

                    if (i % 200 == 0)
                    {
                        context.SaveChanges();
                    }
                }
            }

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext MassCreateCategories(this ApplicationDbContext context, int count)
        {
            var categories = new List<Category>();
            for (int i = 0; i < count; i++)
            {
                var category = new Category()
                {
                    Name = "Category " + (i + 1),
                    Creator = webshopOwnderUser,
                    Attributes = new List<ProductAttribute>()
                    {
                        new ProductAttributeString() {Name = "fixed string" },
                        new ProductAttributeString() {Name = "non-fixed string" },
                    }
                };

                categories.Add(category);


                if (i % 50 == 0)
                {
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                    categories = new List<Category>();
                }
            }
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext MassCreateBrands(this ApplicationDbContext context, int count)
        {
            var brands = new List<Brand>();
            for (int i = 0; i < count; i++)
            {
                var brand = new Brand()
                {
                    Name = "Brand " + (i + 1)
                };
                brands.Add(brand);
            }

            context.Brands.AddRange(brands);
            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateBrands(this ApplicationDbContext context)
        {
            var brands = new List<Brand>()
            {
                new Brand(){ Name = "Lenovo", Creator = webshopOwnderUser },
                new Brand(){ Name = "Apple" , Creator = webshopOwnderUser },
                new Brand(){ Name = "Asus", Creator = webshopOwnderUser },
                new Brand(){ Name = "LG" , Creator = webshopOwnderUser },
                new Brand(){ Name = "Samsung" , Creator = webshopOwnderUser },
                new Brand(){ Name = "Sony", Creator = webshopOwnderUser },
                new Brand(){ Name = "JBL" , Creator = webshopOwnderUser },
                new Brand(){ Name = "Huawei", Creator = webshopOwnderUser },
            };

            context.Brands.AddRange(brands);

            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext CreateProducts(this ApplicationDbContext context)
        {
            var computer1 = new Product()
            {
                CreatedAt = DateTime.Now,
                BrandId = context.Brands.Where(e => e.Name == "Apple").Single().Id,
                CategoryId = context.Categories.Where(e => e.Name == "Laptops").Single().Id,
                Name = "MacBook Air 13\"",
                Offers = new List<Offer>() {
                    new Offer() {SellerId =  webshopOwnderUser.Id, Price = 800, Url = "http://google.com"}
                },
                SmallestPrice = 800,
                PropertyValueConnections = new List<ProductAttributeValueConnection>()
                {
                    new ProductAttributeValueConnection()
                    {
                        ProductAttributeValue =  new ProductAttributeStringValue()
                        {
                            StringValue = "Intel Core™ i5-5350U",
                            Attribute = context.ProductAttributes.Where(e => e.Name == "Processor").Single()
                        }
                    }
                }
            };
            context.Products.Add(computer1);

            var computer2 = new Product()
            {
                CreatedAt = DateTime.Now,
                BrandId = context.Brands.Where(e => e.Name == "Lenovo").Single().Id,
                CategoryId = context.Categories.Where(e => e.Name == "Laptops").Single().Id,
                Offers = new List<Offer>() {
                    new Offer() {SellerId =  webshopOwnderUser.Id, Price = 600, Url = "http://google.com"}
                },
                SmallestPrice = 600,
                Name = "Z50",
                Creator = webshopOwnderUser,
                PropertyValueConnections = new List<ProductAttributeValueConnection>()
                {
                     new ProductAttributeValueConnection()
                     {
                        ProductAttributeValue =  new ProductAttributeStringValue()
                         {
                            StringValue = "Intel i7-4510U",
                            Attribute = context.ProductAttributes.Where(e => e.Name == "Processor").Single()
                        }
                     }
                }
            };
            context.Products.Add(computer2);

            var mobilePhone1 = new Product()
            {
                CreatedAt = DateTime.Now,
                BrandId = context.Brands.Where(e => e.Name == "Huawei").Single().Id,
                CategoryId = context.Categories.Where(e => e.Name == "Phones").Single().Id,
                Name = "Huawei P20 pro",
                Offers = new List<Offer>() {
                    new Offer() {SellerId =  webshopOwnderUser.Id, Price = 1100, Url = "http://google.com"}
                },
                SmallestPrice = 1100,
                PropertyValueConnections = new List<ProductAttributeValueConnection>()
                {
                    new ProductAttributeValueConnection()
                    {
                        ProductAttributeValue =  new ProductAttributeStringValue()
                        {
                            StringValue = "40 MP + 20 MP + 8 MP",
                            Attribute = context.ProductAttributes.Where(e => e.Name == "Back camera").Single()
                        }
                    }
                }
            };
            context.Products.Add(mobilePhone1);

            var book1 = new Product()
            {
                CreatedAt = DateTime.Now,
                CategoryId = context.Categories.Where(e => e.Name == "Books").Single().Id,
                Name = "The Order of the Phoenix",
                Offers = new List<Offer>() {
                    new Offer() {SellerId =  webshopOwnderUser.Id, Price = 5, Url = "http://google.com"}
                },
                SmallestPrice = 5,
                PropertyValueConnections = new List<ProductAttributeValueConnection>()
                {
                    new ProductAttributeValueConnection()
                    {
                        ProductAttributeValue =  new ProductAttributeStringValue()
                        {
                            StringValue = "J. K. Rowling",
                            Attribute = context.ProductAttributes.Where(e => e.Name == "Author").Single()
                        }
                    },
                    new ProductAttributeValueConnection()
                    {
                        ProductAttributeValue =  new ProductAttributeIntValue()
                        {
                            IntValue = 2003,
                            Attribute = context.ProductAttributes.Where(e => e.Name == "Publication date").Single()
                        }
                    }
                }
            };
            context.Products.Add(book1);
            context.SaveChanges();
            return context;
        }

        private static ApplicationDbContext MassCreateProducts(this ApplicationDbContext context, int count)
        {
            var r = new Random();
            var creatorId = context.Users.Single(e => e.NickName == "David Owner").Id;

            var productsToBeAdded = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                int catCount = context.Categories.Count();
                int randomCatIndex = r.Next(0, catCount - 1);
                var category = context.Categories
                    .Include(e => e.Attributes)
                    .Skip(randomCatIndex).First();

                var fixedAttrId = category.Attributes.Single(e => e.Name == "fixed string").Id;
                var fixedAttrValues = context.ProductAttributeValues.Where(e => e.AttributeId == fixedAttrId).ToList();
                var chosenFixedAttrValue = fixedAttrValues.Skip(r.Next(0, fixedAttrValues.Count() - 1)).First();

                int brandCount = context.Brands.Count();
                int randomBrandIndex = r.Next(0, brandCount - 1);
                var brand = context.Brands.Skip(randomBrandIndex).First();


                var product = new Product()
                {
                    CreatedAt = DateTime.Now,
                    BrandId = brand.Id,
                    CategoryId = category.Id,
                    Name = "Dummy Product " + (i + 1),
                    PropertyValueConnections = new List<ProductAttributeValueConnection>()
                    {
                         //a non fixed value attr
                         new ProductAttributeValueConnection()
                         {
                             ProductAttributeValue = new ProductAttributeStringValue()
                             {
                                 StringValue = r.Next(0,100000).ToString(),
                                 AttributeId = category.Attributes.Single(e => e.Name == "non-fixed string").Id
                             }
                         },
                         // a fixed value attr 
                         new ProductAttributeValueConnection()
                         {
                              ProductAttributeValueId = chosenFixedAttrValue.Id
                         }
                    },
                    CreatorId = creatorId,
                    SmallestPrice = r.Next(0, 1000)
                };
                productsToBeAdded.Add(product);
                if (i % 500 == 0)
                {
                    Console.WriteLine($"Added {i} products");
                    context.AddRange(productsToBeAdded);
                    context.SaveChanges();
                    productsToBeAdded = new List<Product>();
                }
            }

            context.AddRange(productsToBeAdded);
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
                   AuthorId = users[r.Next(0, users.Count)].Id,
                   ProductId = products[r.Next(0, products.Count)].Id,
                   Mood = (ReviewMood)r.Next(1, 3),
                   CreatedAt = DateTime.Now.AddSeconds(1 - r.Next(50000)),
                   Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                   "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." +
                   " Eu turpis egestas pretium aenean pharetra magna ac placerat.",
                   Points = r.Next(0, 61)
               })
               .ToList();

            context.Reviews.AddRange(reviews);
            context.SaveChanges();

            return context;
        }

        private static ApplicationDbContext CreateScrores(this ApplicationDbContext context, int numOfScoresPerProduct)
        {
            var r = new Random();

            var products = context.Products.ToList();
            foreach (var p in products)
            {
                p.Scores = new List<Scorereview>();
                for (int i = 0; i < numOfScoresPerProduct; i++)
                {
                    var score = r.Next(1, 6);
                    p.Scores.Add(new Scorereview()
                    {
                        Value = score,
                        CreatedAt = DateTime.Now.AddDays(-score),
                        AuthorId = context.Users.First().Id
                    });
                }
            }

            foreach (var p in products)
            {
                p.ScoreValue = p.Scores.Average(e => e.Value);
            }

            context.SaveChanges();
            return context;
        }
    }
}
