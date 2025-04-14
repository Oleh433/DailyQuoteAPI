using DailyQuote.Domain.Enums;
using DailyQuote.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyQuote.Infrastructure.Identity
{
    public class IdentityInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentityInitializer(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task CreateRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync(RoleOptions.User.ToString()))
            {
                await _roleManager.CreateAsync(new ApplicationRole(RoleOptions.User.ToString()));
            }

            if (!await _roleManager.RoleExistsAsync(RoleOptions.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new ApplicationRole(RoleOptions.Admin.ToString()));
            }

            if (!await _roleManager.RoleExistsAsync(RoleOptions.Owner.ToString()))
            {
                await _roleManager.CreateAsync(new ApplicationRole(RoleOptions.Owner.ToString()));
            }
        }

        public async Task AddOwnerAsync()
        {
            IEnumerable<ApplicationUser> admins = await _userManager.GetUsersInRoleAsync(RoleOptions.Owner.ToString());

            if (admins.Count() == 0)
            {
                ApplicationUser user = new()
                {
                    UserName = _configuration["Owner:Email"],
                    Email = _configuration["Owner:Email"]
                };

                await _userManager.CreateAsync(user, _configuration["Owner:Password"]);

                await _userManager.AddToRoleAsync(user, RoleOptions.Owner.ToString());
            }
        }
    }
}
