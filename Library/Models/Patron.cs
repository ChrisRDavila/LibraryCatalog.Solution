using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Patron
  {
    public int PatronId { get; set; }
    public string PName { get; set; }
    public List<CopyPatron> JoinEntities { get; }
    public ApplicationUser User { get; set; }
    
  }
}