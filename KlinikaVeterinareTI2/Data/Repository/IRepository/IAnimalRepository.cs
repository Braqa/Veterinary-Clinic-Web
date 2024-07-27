using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        void Update(Animal animal);
        IEnumerable<SelectListItem> GetAnimalListForDropdown();
        IEnumerable<SelectListItem> GetAnimalGenderListForDropdown();
    }
}