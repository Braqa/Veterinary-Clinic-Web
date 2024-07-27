using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface IVisitRepository : IRepository<Visit>
    {
        void Update(Visit visit);
        IEnumerable<SelectListItem> GetVisitDatesForDropdown();
    }
}
