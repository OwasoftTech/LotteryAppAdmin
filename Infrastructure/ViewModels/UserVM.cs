using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            Roles = new List<UserRoleVM>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "User's Name is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "User's Email is Required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User's Password is Required!")]
        public string Password { get; set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int RoleId { get; set; }
        public string Role { get; set; } = "-";
        public IEnumerable<UserRoleVM> Roles { get; set; }
    }
}
