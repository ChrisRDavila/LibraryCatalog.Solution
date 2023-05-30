using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Library.Models
{
  public class Author
  {
    public int AuthorId { get; set; } 
    public string AFirstName { get; set; }
    public string ALastName { get; set; }
    public List<AuthorBook> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}