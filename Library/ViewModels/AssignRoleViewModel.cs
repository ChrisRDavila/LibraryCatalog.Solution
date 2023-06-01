using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Library.ViewModels
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        
    }
}
