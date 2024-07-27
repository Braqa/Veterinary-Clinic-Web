using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.Models
{
    public class Race
    {
        public int Id { get; set; }

        public int FamilyId { get; set; }
        public Family Family { get; set; }

        [StringLength(50)]
        [Display(Name = "Raca")]
        public string Name { get; set; }
    }
}
