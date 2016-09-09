using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAPIAngularJSAuthentication.Models;

namespace WebAPIAngularJSAuthentication.Repositories
{
    public class AuthRepository : IDisposable
    {
        private UserManager<IdentityUser> _userManager;
        private AuthContext _ctxAuthContext;

        public AuthRepository()
        {
            _ctxAuthContext = new AuthContext();
            _userManager=new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctxAuthContext));
        }

        public async Task<IdentityResult> RegisterUserTask(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        public void Dispose()
        {
            _ctxAuthContext.Dispose();
            _userManager.Dispose();
        }
    }
}