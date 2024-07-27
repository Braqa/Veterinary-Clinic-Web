using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class VisitViewModel
    {
        public Visit Visit { get; set; }
        public IEnumerable<SelectListItem> AnimalList { get; set; }
        public IEnumerable<SelectListItem> VeterinarianList { get; set; }
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}
