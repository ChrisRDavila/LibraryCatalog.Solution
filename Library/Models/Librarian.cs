using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Librarian
    {
        public int LibrarianId { get; set; }
        [Required(ErrorMessage = "The librarian name can't be empty!")]
        public string LName { get; set; }
        public List<LibrarianCopy> JoinLibrarianCopy { get; }
        public ApplicationUser User { get; set; } 
    }
}