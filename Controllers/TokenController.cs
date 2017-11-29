using System.Collections.Generic;
using System.Linq;
using LojaNemesis.Auth;
using LojaNemesis.Infra;
using LojaNemesis.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
  [Route("token")]
  public class TokenController : Controller
  {
    private AppDbContext context;

    public TokenController(AppDbContext context) { this.context = context; }

    [HttpPost]
    public IActionResult Post([FromBody]LoginViewModel model)
    {
      if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
        return Unauthorized();

      var user = context.Usuario.FirstOrDefault(p => p.Login == model.Login);
      if (user == null || user.Password != model.Password)
        return Unauthorized();

      var claims = new Dictionary<string, string>();
      claims.Add("UserId", user.Id.ToString());
      claims.Add("Role", user.Tipo);
      var token = new JwtTokenBuilder(claims).Build();
      return Ok(token.Value);
    }
  }
}