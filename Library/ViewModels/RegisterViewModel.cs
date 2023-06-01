using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace Library.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", ErrorMessage = "Your password must contain at least six characters, a capital letter, a lowercase letter, a number, and a special character.")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public bool IsAdmin { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Admin Password")]
    public string AdminPassword { get; set; }
    public IList<string> UserRoles {get; set;}

    public IEnumerable<IdentityRole> Roles { get; set; }

  }
}