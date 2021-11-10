using EmailSystemDotNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Repository
{
    public interface IUserRepository
    {
        public Task<UserModel> getLoggedUser(ClaimsPrincipal claims);
        public Task<UserModel> getUserByEmail(string email);
    }
}
