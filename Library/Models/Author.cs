using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Library.Models
{
  public class Author
  {
    public int AuthorId { get; set; }
    [Required(ErrorMessage = "The author's first name can't be empty!")]
    public string AFirstName { get; set; }
    [Required(ErrorMessage = "The author's last name can't be empty!")]
    public string ALastName { get; set; }
    public List<AuthorBook> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}