using Microsoft.AspNetCore.Identity;
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
                    .CreateUsers();               
                   
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
    }
}
