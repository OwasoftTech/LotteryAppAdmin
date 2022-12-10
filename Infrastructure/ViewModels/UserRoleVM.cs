using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class UserRoleVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role Name is required!")]
        public string Name { get; set; } = null!;
    }
    public class UserRoleCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Role Name is required!")]
        public string Name { get; set; } = null!;
    }
}
