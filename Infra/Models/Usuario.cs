namespace LojaNemesis.Infra.Models
{
  public class Usuario
  {
    public int Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public string Tipo { get; set; }
    public string Password { get; set; }
  }
}