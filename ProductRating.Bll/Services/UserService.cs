using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Enum;
using ProductRating.Bll.Dtos.Users;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Dal;
using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<UserManageHeaderDto>> AdminGetUsers(UserManageFilterDto filter, PaginationDto pagination)
        {
            var ownerRole = await context.Set<ApplicationRole>().SingleAsync(e => e.Name == RoleNames.SHOP_OWNER_ROLE);
            var customerRole = await context.Set<ApplicationRole>().SingleAsync(e => e.Name == RoleNames.USER_ROLE);         
                        

            var query = context.Users
                .Where(e => context.UserRoles.Where(d => d.UserId == e.Id).Any(r => r.RoleId == ownerRole.Id || r.RoleId == customerRole.Id))
                .AsQueryable();

            var now = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(e => e.Email.Contains(filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.NickName))
            {
                query = query.Where(e => e.NickName.ToUpper().Contains(filter.NickName.ToUpper()));
            }
            if (filter.IsLockedOut != null)
            {               
                query = filter.IsLockedOut.Value
                    ? query.Where(e => e.LockoutEnd < now)
                    : query.Where(e => e.LockoutEnd >= now);
            }

            if (filter.Role != null)
            {
                query = filter.Role == Role.WebshopOwner
                    ? query.Where(e => context.UserRoles.Where(d => d.UserId == e.Id).Any(r => r.RoleId == ownerRole.Id ))
                    : query.Where(e => context.UserRoles.Where(d => d.UserId == e.Id).Any(r => r.RoleId == customerRole.Id));
            }

            return await query.Select(e => new UserManageHeaderDto()
            {
                Id = e.Id,
                NickName = e.NickName,
                Email = e.Email,
                IsLockedOut = e.LockoutEnd < now,
                Role = context.UserRoles.Where(d => d.UserId == e.Id).Any(r => r.RoleId == ownerRole.Id) 
                    ? Role.WebshopOwner.ToString()
                    : Role.Customer.ToString()
            }).ToListAsync();
        }

        public async Task LockoutUser(Guid userId)
        {
            var user = await context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            user.LockoutEnabled = true;
            user.LockoutEnd = new DateTime(2100, 1, 1);
            await context.SaveChangesAsync();
        }

        public async Task AdmitUser(Guid userId)
        {
            var user = await context.Users.SingleOrDefaultAsync(e => e.Id == userId);
            user.LockoutEnd = DateTime.Now.AddDays(-1);
            await context.SaveChangesAsync();
        }
    }
}
