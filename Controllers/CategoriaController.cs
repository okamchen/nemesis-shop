using System;
using System.Collections.Generic;
using System.Linq;
using LojaNemesis.Helpers;
using LojaNemesis.Infra;
using LojaNemesis.Infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class CategoriaController : Controller
  {
    private AppDbContext context;
    public CategoriaController(AppDbContext context)
    {
      this.context = context;
    }

    [HttpGet]
    public List<Categoria> Get()
    {
      return context.Categoria.ToList();
    }

    [HttpGet("{id}")]
    public Categoria Get(int id)
    {
      return context.Categoria.FirstOrDefault(p => p.Id == id);
    }

    [HttpPost]
    public void Post([FromBody] Categoria categoria)
    {
      if (string.IsNullOrWhiteSpace(categoria.Nome))
        throw new ValidateException("A nova categoria precisa de um nome.");
      if (context.Categoria.FirstOrDefault(p => p.Nome == categoria.Nome) != null)
        throw new ValidateException("Já existe uma categoria com este nome.");
      context.Categoria.Add(new Categoria { Nome = categoria.Nome });
      context.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      var c = context.Categoria.FirstOrDefault(p => p.Id == id);
      context.Categoria.Remove(c);
      context.SaveChanges();
    }
  }
}