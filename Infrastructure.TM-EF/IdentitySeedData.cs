using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TM_EF
{
    public class IdentitySeedData : ISeedData
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentitySeedData(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task EnsurePopulated(bool dropExisting = false)
        {
            const string CLAIMNAME_USERTYPE = "UserType";
            const string PASSWORD = "Secret123$";

            const string USERNAME_POWERUSER = "admin@gmail.com";
            const string USERNAME_REGULARUSER = "user@gmail.com";

            var powerUser = await _userManager.FindByNameAsync(USERNAME_POWERUSER);
            if (powerUser != null)
                await _userManager.DeleteAsync(powerUser);

            var regularUser = await _userManager.FindByNameAsync(USERNAME_REGULARUSER);
            if (regularUser != null)
                await _userManager.DeleteAsync(regularUser);

            IdentityUser _powerUser = await _userManager.FindByIdAsync(USERNAME_POWERUSER);
            if (_powerUser == null)
            {
                _powerUser = new IdentityUser(USERNAME_POWERUSER);

                await _userManager.CreateAsync(_powerUser, PASSWORD);
                await _userManager.AddClaimAsync(_powerUser, new Claim(CLAIMNAME_USERTYPE, "poweruser"));
            }

            IdentityUser _regularUser = await _userManager.FindByIdAsync(USERNAME_REGULARUSER);
            if (_regularUser == null)
            {
                _regularUser = new IdentityUser(USERNAME_REGULARUSER);

                await _userManager.CreateAsync(_regularUser, PASSWORD);
                await _userManager.AddClaimAsync(_regularUser, new Claim(CLAIMNAME_USERTYPE, "regularuser"));
            }

        }
    }

}
