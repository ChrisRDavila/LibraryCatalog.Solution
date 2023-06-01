using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Checkout
  {
    public int CheckoutId { get; set; }
    public int CopyId { get; set; }
    public int PatronId { get; set; }
    [Required(ErrorMessage = "The checkout's date can't be empty!")]
    public DateTime CheckoutDate { get; set; }
    [Required(ErrorMessage = "The due date can't be empty!")]
    public DateTime DueDate { get; set; }
    public bool IsOverdue { get; set; }
    public bool Status { get; set; }
    public List<BookCheckout> JoinBookCheckout { get; }
    public List<PatronCheckout> JoinPatronCheckout { get; }
    public ApplicationUser User { get; set; }
  }
}
