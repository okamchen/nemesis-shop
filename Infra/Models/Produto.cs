using System;

namespace LojaNemesis.Infra.Models
{
  public class Produto
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public Preco Valor { get; set; }
    public DateTime DataValidade { get; set; }
    public Categoria Categoria { get; set; }
  }
}