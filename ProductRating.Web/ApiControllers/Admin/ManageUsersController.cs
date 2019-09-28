using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Users;
using ProductRating.Bll.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
    [Authorize]
    [Route("manage-users")]
    public class ManageUsersController : Controller
    {
        private readonly IUserService userService;

        public ManageUsersController(IUserService userService) 
        {
            this.userService = userService;
        }
   
        [HttpPost("list")]
        [ProducesResponseType(typeof(List<UserManageHeaderDto>), 200)]
        public async Task<IActionResult> Listusers([FromBody] UserManageFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var users = await userService.AdminGetUsers(filter, pagination);
            return Ok(users);
        }

        [HttpPost("{userId}/lockout")]     
        public async Task<IActionResult> LockoutUser(Guid userId )
        {
            await userService.LockoutUser(userId);
            return Ok();
        }

        [HttpPost("{userId}/admit")]
        public async Task<IActionResult> AdmitUser(Guid userId)
        {
            await userService.AdmitUser(userId);
            return Ok();
        }
    }
}



