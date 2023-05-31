using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Book
  {
    public int BookId { get; set; }
    [Required(ErrorMessage = "The book's title can't be empty!")]
    public string Title { get; set; }
    [Required(ErrorMessage = "The book's publication year can't be empty!")]
    public int PublicationYear { get; set; }
    public List<AuthorBook> JoinEntities { get; }
    public List<Copy> Copies { get; set; }
    public ApplicationUser User { get; set; }
  }
}