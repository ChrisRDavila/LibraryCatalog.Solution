using System;

namespace Library.Models
{
    public class CopyPatron
  {
    public int CopyPatronId { get; set; }
    public int CopyId { get; set; }
    public int PatronId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsOverdue { get; set; }
    public bool Status { get; set; }
    public Copy Copy { get; set; }
    public Patron Patron { get; set; }
    public ApplicationUser User { get; set; }
  }
}
