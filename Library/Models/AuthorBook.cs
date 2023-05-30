namespace Library.Models
{
  public class AuthorBook
  {
    public int ABId {get; set; }
    public int AId { get; set; }
    public int BId { get; set; }
    public Author Author { get; set; }
    public Book Book { get; set; }
  }
}