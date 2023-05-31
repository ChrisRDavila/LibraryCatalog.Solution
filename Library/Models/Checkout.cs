using System;
using System.Collections.Generic;

namespace Library.Models
{
    public class Checkout
  {
    public int CheckoutId { get; set; }
    // public int CopyId { get; set; }
    // public int PatronId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsOverdue { get; set; }
    public bool Status { get; set; }
    public List<BookCheckout> JoinBookCheckout { get; }
    public List<PatronCheckout> JoinPatronCheckout { get; }
    public ApplicationUser User { get; set; }
  }
}
