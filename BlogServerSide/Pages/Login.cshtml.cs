using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogServerSide.DataBase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BlogServerSide.Pages
{
    public class LoginModel : PageModel
    {
     
        IConfiguration Configuration { get; set; }

        private IConfigurationRoot ConfigRoot;

        public LoginModel(IConfiguration configRoot)
        {
            Configuration = configRoot;
        }


        public IActionResult OnGetAsync(string returnUrl = null)
        {
            string provider = "Google";
            // Request a redirect to the external login provider.
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("./Login",
                pageHandler: "Callback",
                values: new { returnUrl }),
            };
            return new ChallengeResult(provider, authenticationProperties);
        }
        public async Task<IActionResult> OnGetCallbackAsync(
            string returnUrl = null, string remoteError = null)
        {
            // Get the information about the user from the external login provider
            var GoogleUser = this.User.Identities.FirstOrDefault();


            //handle users here?

            using (var context = new BlogContext(Configuration))
            {
                context.Database.EnsureCreated();

                var email = GoogleUser.Claims.First(c => c.Type == ClaimTypes.Email)?.Value;
                var id = GoogleUser.Claims.First(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var firsName = GoogleUser.Claims.First(c => c.Type == ClaimTypes.GivenName)?.Value;
                var lastName = GoogleUser.Claims.First(c => c.Type == ClaimTypes.Surname)?.Value;



                if (context.Users.Where(u => u.Id == id).Count() == 0)
                {
                    context.Users.Add(new DataBase.User()
                    {
                        Id = id,
                        Email = email,
                        FirstName = firsName,
                        LastName = lastName,
                        RegisteredAt = DateTime.Now

                    });
                    context.SaveChanges();  
                }
                else
                {
                    var user = context.Users.FirstOrDefault(x=>x.Id == id);
                }

            }


            if (GoogleUser.IsAuthenticated)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(GoogleUser),
                authProperties);
            }
            return LocalRedirect("/");
        }
    }
}
