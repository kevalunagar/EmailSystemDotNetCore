using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailSystemDotNetCore.Models
{
    public class ApplicationUserClaims:UserClaimsPrincipalFactory<UserModel>
    {
        public ApplicationUserClaims(UserManager<UserModel> userManager
            ,IOptions<IdentityOptions> options):base(userManager,options)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity= await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("userProfile", user.ImagePath));
            identity.AddClaim(new Claim("userFirstName", user.FirstName));
            identity.AddClaim(new Claim("userFirstName", user.FirstName));
            identity.AddClaim(new Claim("userLastName", user.LastName));
            return identity;
        }
    }
}
