using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin.Controllers
{
    public class AccountController : ControllerBase
    {
        SignInManager<IdentityUser> signInManager;

        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> Login([FromForm]LoginInfoModel model, [FromQuery]string callback)
        {
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (!result.Succeeded)
            {
                return NotFound();
            }
            await signInManager.SignInAsync(new IdentityUser(model.Username), false);
            return Redirect(callback);
        }

        public async Task<IActionResult> Logout([FromQuery]string callback)
        {
            await signInManager.SignOutAsync();
            return Redirect(callback);
        }
    }
}
