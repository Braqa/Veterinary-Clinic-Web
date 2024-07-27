using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

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

        public IList<string> Roles { get; set; }
    }
}
