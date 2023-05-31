using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Patron
  {
    public int PatronId { get; set; }
    [Required(ErrorMessage = "The patrons name can't be empty!")]
    public string PName { get; set; }
    public List<BookPatron> JoinBookPatron { get; }
    public List<PatronCheckout> JoinPatronCheckout { get; }
    public ApplicationUser User { get; set; }
    
  }
}