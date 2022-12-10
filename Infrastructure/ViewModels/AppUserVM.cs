using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class AppUserVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
        public string Phone { get; set; }
        public string CountryCode { get; set; }
        public string ProfileImgPath { get; set; }
    }
    public class AppUserSignUpVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CountryCode { get; set; }
    }
    public class AppUserUpdateVM
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string photoName { get; set; }
    }
    public class AppUserUpdateTempVM
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Byte[] PhotoArry { get; set; }
        public string PhotoExtension { get; set; }
    }
    public class AppUserSignin
    {
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string Phone { get; set; }
    }

}
