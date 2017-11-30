using System.Linq;
using LojaNemesis.Infra;
using LojaNemesis.Infra.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
  [Route("api/[controller]")]
  public class PedidoController : Controller
  {
    private AppDbContext context;

    public PedidoController(AppDbContext context)
    {
      this.context = context;
    }

    public IActionResult Get()
    {
      return Ok(context.Produto.ToList());
    }
  }
}
