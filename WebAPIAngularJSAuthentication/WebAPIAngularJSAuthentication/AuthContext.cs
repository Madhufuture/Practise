using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAPIAngularJSAuthentication
{
    public class AuthContext:IdentityDbContext<IdentityUser>
    {
        public AuthContext() 
            : base("AuthContext")
        {
            
        }
    }
}