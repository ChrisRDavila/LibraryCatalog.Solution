using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;

public class Checkout
{
  public int Id { get; set; }
  public int CopyId { get; set; }
  public int PatronId { get; set; }
  public DateTime CheckoutDate { get; set; }
  public DateTime DueDate { get; set; }
  public bool IsOverdue { get; set; }
  public string Status { get; set; }
  public Copy Copy { get; set; }
  public Patron Patron { get; set; }
}