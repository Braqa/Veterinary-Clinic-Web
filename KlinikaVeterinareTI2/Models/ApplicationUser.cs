using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KlinikaVeterinareTI2.Utilities;
using Microsoft.AspNetCore.Identity;

namespace KlinikaVeterinareTI2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Emri")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mbiemri")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Ditelindja")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Gjinia")]
        public string Gender { get; set; }

        [NotMapped]
        [Display(Name = "Pronari")]
        public string FullName
        {
            get => FirstName + " " + LastName;
        }

        public bool IsDeleted { get; set; }
    }
}
