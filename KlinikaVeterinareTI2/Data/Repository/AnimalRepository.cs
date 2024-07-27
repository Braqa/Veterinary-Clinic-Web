using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Animal animal)
        {
            var animalFromDb = _context.Animals.FirstOrDefault(x => x.Id == animal.Id);
            animalFromDb.Name = animal.Name;
            animalFromDb.FamilyId = animal.FamilyId;
            animalFromDb.RaceId = animal.RaceId;
            animalFromDb.Birthday = animal.Birthday;
            animalFromDb.UserId = animal.UserId;
            animalFromDb.Gender = animal.Gender;
            animalFromDb.ImageUrl = animal.ImageUrl;
            animalFromDb.LUB = animal.LUB;
            animalFromDb.LUD = animal.LUD;
            _context.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetAnimalListForDropdown()
        {
            return _context.Animals.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetAnimalGenderListForDropdown()
        {
            return _context.Animals.Select(x => new SelectListItem()
            {
                Text = x.Gender,
                Value = x.Gender
            });
        }
    }
}
