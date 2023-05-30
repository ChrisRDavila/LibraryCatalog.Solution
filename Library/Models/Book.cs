using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public int BId { get; set; }
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public List<AuthorBook> JoinEntites { get; }
    public List<Copy> Copies { get; set; }
  }
}