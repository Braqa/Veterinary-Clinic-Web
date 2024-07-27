using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.Models
{
    public class Visit : BaseAudit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Data e vizites")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Kafsha")]
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        [Required]
        [Display(Name = "Pronari")]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        [Required]
        [Display(Name = "Veterinari")]
        public string VeterinarianId { get; set; }
        [ForeignKey("VeterinarianId")]
        public ApplicationUser Veterinarian { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Diagnoza")]
        public string Diagnosis { get; set; }
    }
}
