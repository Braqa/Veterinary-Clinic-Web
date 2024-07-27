using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        void Update(Vaccine vaccine);
    }
}
