using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VoterListApp.Areas.Identity.Data;

namespace VoterListApp.Services
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser/*,IdentityRole*/>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,IOptions<IdentityOptions> options) : base(userManager
         , options)
        {
                    



        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserFirstName" , user.FirstName ?? ""));
            identity.AddClaim(new Claim("UserLastName", user.LastName ?? ""));
            identity.AddClaim(new Claim("UserImageName", user.UserImageName ?? ""));
            return identity;
        }
    }
}
