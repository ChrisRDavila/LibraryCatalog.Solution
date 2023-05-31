using System.Collections.Generic;

namespace Library.Models
{
  public class Copy
  {
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public string Serial { get; set; }
    public int Quantity { get; set; }
    public Book Book { get; set; }
    public List<CopyPatron> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}