namespace Library.Models
{
  public class LibrarianCopy
    {       
        public int LibrarianCopyId { get; set; }
        public int CopyId { get; set; }
        public Copy Copy { get; set; }
        public int LibrarianId { get; set; }
        public Librarian Librarian { get; set; }
    }
}