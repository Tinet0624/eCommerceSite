using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; } // Optional
    }

    public class RegisterViewModel
    { // Does not go in DB do not add DbSet for this!!
        [Required]
        public string Email { get; set; }

        [Compare(nameof(Email))] // Might need to put required...
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))] // Might need to put required...
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)] // Time ignored
        public DateTime? DateOfBirth { get; set; } // Optional
    }
}
