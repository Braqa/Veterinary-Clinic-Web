using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Shkruaj emrin e rolit!")]
        [Display(Name = "Emri Rolit")]
        public string RoleName { get; set; }
    }
}
