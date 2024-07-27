using System;
using System.ComponentModel.DataAnnotations;

namespace KlinikaVeterinareTI2.Models
{
    public class Vaccine : BaseAudit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Numri Serik")]
        public string SerialNo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emri")]
        public string Name { get; set; }

        [Display(Name = "Pershkrimi")]
        public string Description { get; set; }
    }
}
