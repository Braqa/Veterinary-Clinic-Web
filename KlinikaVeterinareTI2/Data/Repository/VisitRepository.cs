using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        private readonly ApplicationDbContext _context;

        public VisitRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Visit visit)
        {
            var visitFromDb = _context.Visits.FirstOrDefault(x => x.Id == visit.Id);
            visitFromDb.Date = visit.Date;
            visitFromDb.AnimalId = visit.AnimalId;
            visitFromDb.VeterinarianId = visit.VeterinarianId;
            visitFromDb.Diagnosis = visit.Diagnosis;
            visitFromDb.LUB = visit.LUB;
            visitFromDb.LUD = visit.LUD;
            _context.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetVisitDatesForDropdown()
        {
            return _context.Visits.Select(x => new SelectListItem()
            {
                Text = x.Date.Year.ToString(),
                Value = x.Date.Year.ToString()
            }).Distinct();
        }
    }
}
