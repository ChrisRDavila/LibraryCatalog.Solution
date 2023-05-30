using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Book
  {
    public int BookId { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public List<AuthorBook> JoinEntities { get; }
    public List<Copy> Copies { get; set; }
    public ApplicationUser User { get; set; }
  }
}