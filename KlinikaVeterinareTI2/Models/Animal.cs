using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.Models
{
    public class Animal : BaseAudit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emri")]
        public string Name { get; set; }

        [Display(Name = "Familja")]
        public int FamilyId { get; set; }
        public Family Family { get; set; }

        [Display(Name = "Raca")]
        public int RaceId { get; set; }
        public Race Race { get; set; }

        [Required]
        [Display(Name = "Ditelindja")]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Pronari")]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Display(Name = "Gjinia")]
        [StringLength(20)]
        public string Gender { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }
    }
}
