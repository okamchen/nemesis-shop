using System.ComponentModel.DataAnnotations;

namespace LojaNemesis.ViewModel.Validation
{
  public class MinFieldLength : MinLengthAttribute
  {
    public MinFieldLength(string fieldName, int length) : base(length)
    {
      ErrorMessage = $"O campo {fieldName} deve ter no mínimo {length} caracteres";
    }
  }
}