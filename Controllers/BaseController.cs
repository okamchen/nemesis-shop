using System.Linq;
using LojaNemesis.Helpers;
using LojaNemesis.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaNemesis.Controllers
{
  public abstract class BaseController : Controller
  {
    protected AppDbContext context;
    public BaseController(AppDbContext context) => this.context = context;

    protected void ValidateModel()
    {
      if (!ModelState.IsValid)
        throw new ValidateException()
        {
          Errors = ModelState.Values.SelectMany(p => p.Errors).Select(p => p.ErrorMessage)
        };
    }
  }
}