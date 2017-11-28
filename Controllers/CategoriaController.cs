using System;
using System.Collections.Generic;
using System.Linq;
using LojaNemesis.Infra;
using LojaNemesis.Infra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
        public void Post(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("A nova categoria precisa de um nome.");
            context.Categoria.Add(new Categoria { Nome = nome });
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