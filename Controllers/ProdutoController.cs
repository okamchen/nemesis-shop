using System.Linq;
using LojaNemesis.Infra;
using LojaNemesis.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
  [Route("api/[controller]")]
  public class ProdutoController : Controller
  {
    private AppDbContext context;

    public ProdutoController(AppDbContext context)
    {
      this.context = context;
    }

    public object Get()
    {
      try
      {
        return context.Produto.ToList();
      }
      catch (System.Exception ex)
      {
        return ex;
      }
    }
  }
}
