using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using jwtProject.Data;
using jwtProject.Model;
using Microsoft.AspNetCore.Authorization;

namespace jwtProject.Controllers
{
    [Authorize(Roles = "AdminUser")]
    [Route("api/[controller]")]  // api/roleSetup
    [ApiController]
    public class RoleSetupController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleSetupController> _logger;

        public RoleSetupController(
            ApiDbContext context,
            UserManager<ApiUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RoleSetupController> logger
        )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // Returns all roles
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList(); 
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if (!roleExist) // checks on the role exist status
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                if (roleResult.Succeeded) // check if the role has been added successfully
                {
                    _logger.LogInformation($"The Role {name} has been added successfully");
                    return Ok(new
                    {
                        result = $"The role {name} has been added successfully"
                    });
                }
                else
                {
                    _logger.LogInformation($"The Role {name} has not been added");
                    return BadRequest(new
                    {
                        error = $"The role {name} has not been added"
                    });
                }

            }

            return BadRequest(new { error = "Role already exist" });
        }

        [HttpGet]
        [Route("GetAllUsers")] // api/setup/GetAllUsers
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserRoles")] // api/setup/GetUserRoles
        public async Task<IActionResult> GetUserRoles(string email)
        {
            // check if the email is valid
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) // User does not exist
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            // return the roles
            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

        [HttpPost]
        [Route("AddUserToRole")] // api/setup/AddUserToRole
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            // Check if the user exist
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) // User does not exist
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            // Check if the role exist
            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist) // checks on the role exist status
            {
                _logger.LogInformation($"The role {email} does not exist");
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            // Check if the user is assigned to the role successfully
            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = "Success, user has been added to the role"
                })
                ;
            }
            else
            {
                _logger.LogInformation($"The user was not able to be added to the role");
                return BadRequest(new
                {
                    error = "The user was not able to be added to the role"
                });
            }

        }

        [HttpPost]
        [Route("RemoveUserFromRole")] // api/setup/RemoveUserFromRole
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            // Check if the user exist
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) // User does not exist
            {
                _logger.LogInformation($"The user with the {email} does not exist");
                return BadRequest(new
                {
                    error = "User does not exist"
                });
            }

            // Check if the role exist
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist) // checks on the role exist status
            {
                _logger.LogInformation($"The role {email} does not exist");
                return BadRequest(new
                {
                    error = "Role does not exist"
                });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    result = $"User {email} has been removed from role {roleName}"
                });
            }

            return BadRequest(new
            {
                error = $"Unable to remove User {email} from role {roleName}"
            });
        }
    }
}
