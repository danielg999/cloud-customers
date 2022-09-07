using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Field to inject users service.
        /// </summary>
        private readonly IUsersService _usersService;

        /// <summary>
        /// Constructor of UsersController.
        /// </summary>
        /// <param name="usersService">Service of users.</param>
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <returns>Returns users.</returns>
        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersService.GetAllUsers();

            if (users.Any())
            {
                return Ok(users);
            }

            return NotFound();
        }
    }
}
