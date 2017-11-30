using System.Linq;
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
  public class ProdutoController : BaseController
  {
    public ProdutoController(AppDbContext context) : base(context) { }

    [HttpGet]
    public IActionResult Get() => Ok(context.Produto.Include(p => p.Categoria).Include(p => p.HistoricoPreco).ToList());

    [HttpPost]
    public IActionResult Post([FromBody]ProdutoViewModel model)
    {
      ValidateModel();

      if (model.Preco <= 0)
        throw new ValidateException("O preço deve ser maior que zero");

      var p = new Produto(model);
      context.Entry<Produto>(p).State = EntityState.Added;
      context.Entry<Preco>(p.HistoricoPreco.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    public IActionResult Put([FromBody]ProdutoViewModel model)
    {
      ValidateModel();

      if (model.Preco <= 0)
        throw new ValidateException("O preço deve ser maior que zero");

      var p = new Produto(model);
      context.Entry<Produto>(p).State = EntityState.Modified;
      context.Entry<Preco>(p.HistoricoPreco.First()).State = EntityState.Added;
      context.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (context.VendaProduto.FirstOrDefault(p => p.Produto.Id == id) != null)
        throw new ValidateException("O está vinculado à pedidos e não pode ser excluído.");
      var produto = context.Produto.Include(p => p.HistoricoPreco).FirstOrDefault(p => p.Id == id);
      context.Preco.RemoveRange(produto.HistoricoPreco);
      context.Produto.Remove(produto);
      return Ok();
    }
  }
}