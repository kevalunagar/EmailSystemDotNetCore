using EmailSystemDotNetCore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<UserModel> userManager;
        private readonly AppDbContext appDbContext;

        public UserRepository(UserManager<UserModel> userManager
            ,AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }
        public async Task<UserModel> getLoggedUser(ClaimsPrincipal claims)
        {
            return await userManager.GetUserAsync(claims);
        }

        public async Task<UserModel> getUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

    }
}
