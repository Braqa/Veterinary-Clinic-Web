using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Fjalekalimi")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo fjalekalimin")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Emri")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Mbiemri")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Ditelindja")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Gjinia")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Numri Telefonit")]
        public string PhoneNo { get; set; }
    }
}
