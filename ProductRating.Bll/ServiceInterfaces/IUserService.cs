using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IUserService
    {
        Task<List<UserManageHeaderDto>> AdminGetUsers(UserManageFilterDto filter, PaginationDto pagination);

        Task LockoutUser(Guid userId);

        Task AdmitUser(Guid userId);
    }
}
