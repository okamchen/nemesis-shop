using System;
using System.Collections.Generic;
using System.Linq;
using LojaNemesis.Auth;
using LojaNemesis.Helpers;
using LojaNemesis.Infra;
using LojaNemesis.Infra.Models;
using LojaNemesis.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaNemesis.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class UsuarioController : Controller
  {
    private AppDbContext context;
    public UsuarioController(AppDbContext context)
    {
      this.context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
      var users = context.Usuario
      .Select(p => new UserViewModel(p)).ToList();
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var user = context.Usuario.Find(id);

      if (user == null)
        return NotFound();

      return Ok(new UserViewModel(user));
    }

    [HttpPost]
    public IActionResult Post([FromBody]CreateUserViewModel model)
    {
      if (!ModelState.IsValid)
        throw new ValidateException()
        {
          Errors = (ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage))
        };

      Usuario user = context.Usuario.FirstOrDefault(p => p.Email == model.Email || p.Login == model.Login);

      if (user != null)
      {
        List<string> errors = new List<string>();
        if (user.Login == model.Login)
          errors.Add("O 'Login' informado já está sendo usado.");
        if (user.Email == model.Email)
          errors.Add("O 'Email' informado já está sendo usado.");
        throw new ValidateException() { Errors = errors };
      }

      context.Usuario.Add(new Usuario(model));
      context.SaveChanges();
      return Ok();
    }

    [HttpPut]
    public IActionResult Put([FromBody]UserViewModel model)
    {
      if (!ModelState.IsValid)
        throw new ValidateException()
        {
          Errors = (ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage))
        };

      Usuario user = context.Usuario.FirstOrDefault(p => p.Id != model.Id && (p.Email == model.Email || p.Login == model.Login));

      if (user != null)
      {
        List<string> errors = new List<string>();
        if (user.Login == model.Login)
          errors.Add("O 'Login' informado já está sendo usado.");
        if (user.Email == model.Email)
          errors.Add("O 'Email' informado já está sendo usado.");
        throw new ValidateException() { Errors = errors };
      }

      if (model.Tipo.ToUpper() != "ADMIN" && context.Usuario.FirstOrDefault(p => p.Id != model.Id && p.Tipo.ToUpper() == "ADMIN") == null)
        throw new ValidateException("O único administrador não pode ter seu privilégio removido." );

      user = context.Usuario.Find(model.Id);
      user.Email = model.Email;
      user.Login = model.Login;
      user.Tipo = model.Tipo;
      context.Entry(user).State = EntityState.Modified;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var user = context.Usuario.FirstOrDefault(p => p.Id == id);
      if (user?.Tipo?.ToUpper() == "ADMIN" && context.Usuario.FirstOrDefault(p => p.Id != id && p.Tipo.ToUpper() == "ADMIN") == null)
        throw new ValidateException("O único administrador não pode ser removido." );
      context.Remove(user);
      context.SaveChanges();
      return Ok();
    }
  }
}