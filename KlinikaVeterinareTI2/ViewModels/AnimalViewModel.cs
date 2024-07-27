using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class AnimalViewModel
    {
        public Animal Animal { get; set; }
        public IEnumerable<SelectListItem> FamilyList { get; set; }
        public IEnumerable<SelectListItem> RaceList { get; set; }
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}
