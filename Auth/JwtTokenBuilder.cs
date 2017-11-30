using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace LojaNemesis.Auth
{
  public class JwtTokenBuilder
  {
    private Dictionary<string, string> claims = new Dictionary<string, string>();

    private int expiryInMinutes = 1000;

    public JwtTokenBuilder(Dictionary<string, string> claims)
    {
      this.claims = claims;
    }

    public JwtToken Build()
    {
      var claims = this.claims.Select(item => new Claim(item.Key, item.Value)).ToList();

      var token = new JwtSecurityToken(
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
          signingCredentials: new SigningCredentials(
              JwtSecurityKey.Create(),
              SecurityAlgorithms.HmacSha256
          )
      );
      return new JwtToken(token);
    }
  }
}