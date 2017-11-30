using System;
using LojaNemesis.Infra.Models;
using LojaNemesis.ViewModel.Validation;

namespace LojaNemesis.ViewModel
{
  public class ProdutoViewModel
  {
    public int Id { get; set; }

    [RequiredField("Nome")]
    public string Nome { get; set; }

    [RequiredField("Preco")]
    public decimal Preco { get; set; }

    [RequiredField("DataValidade")]
    public DateTime DataValidade { get; set; }

    [RequiredField("Categoria")]
    public Categoria Categoria { get; set; }
  }
}