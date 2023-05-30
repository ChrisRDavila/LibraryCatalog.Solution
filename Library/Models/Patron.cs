using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public int PId { get; set; }
    public string PUsername { get; set; }
    public string PPassword { get; set; }
    public string PEmail { get; set; }
    public string PName { get; set; }
    public List<Checkout> JoinEntities { get; }

  }
}