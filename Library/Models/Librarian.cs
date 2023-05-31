using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Librarian
    {
        public int LibrarianId { get; set; }
        public string LName { get; set; }
        public List<LibrarianCopy> JoinLibrarianCopy { get; }
        public ApplicationUser User { get; set; } 
    }
}