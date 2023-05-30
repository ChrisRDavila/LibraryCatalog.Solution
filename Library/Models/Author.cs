using System.Collections.Generic;


namespace Library.Models
{
  public class Author
  {
    public int AId { get; set; } 
    public string AName { get; set; }
    public List<AuthorBook> JoinEntities { get; }
  }
}