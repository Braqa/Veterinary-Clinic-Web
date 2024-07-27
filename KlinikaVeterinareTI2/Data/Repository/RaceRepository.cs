using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class RaceRepository : Repository<Race>, IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetRaceListForDropdown()
        {
            return _context.Races.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
    }
}
