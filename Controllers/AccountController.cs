using System.Collections.Generic;
using LojaNemesis.Auth;
using LojaNemesis.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [Route("token")]
        [HttpPost]
        public object Post(LoginViewModel model)
        {
            if (model.Username != "admin" || model.Password != "123")
                return Unauthorized();
            var token =  new JwtTokenBuilder(JwtSecurityKey.Create(), new Dictionary<string, string>(), 3000).Build();
            return Ok(token.Value);
        }
    }
}