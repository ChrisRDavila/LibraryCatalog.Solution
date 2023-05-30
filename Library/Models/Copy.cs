using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public string Status { get; set; }
    public Book Book { get; set; }
    public List<Checkout> JoinEntities { get; }
  }
}