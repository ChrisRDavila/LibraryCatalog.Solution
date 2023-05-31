using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Library.ViewModels
{
    public class AssignRoleViewModel
    {
      public string UserId { get; set; }
      public string UserName { get; set; }
      public IList<string> UserRoles {get; set;}
      public IEnumerable<IdentityRole> Roles { get; set; }

      [Required]
      [DataType(DataType.Password)]
      [Display(Name = "Admin Password")]
      [Compare("AdminPassword", ErrorMessage = "The admin password is incorrect.")]
      public string AdminPassword { get; set; }
    }
}
