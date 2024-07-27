using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        private readonly ApplicationDbContext _context;

        public VaccineRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Vaccine vaccine)
        {
            var objFromDb = _context.Vaccines.FirstOrDefault(x => x.Id == vaccine.Id);
            objFromDb.SerialNo = vaccine.SerialNo;
            objFromDb.Name = vaccine.Name;
            objFromDb.Description = vaccine.Description;
            objFromDb.LUB = vaccine.LUB;
            objFromDb.LUD = vaccine.LUD;
            _context.SaveChanges();
        }
    }
}
