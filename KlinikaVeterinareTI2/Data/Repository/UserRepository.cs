using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetOwnersForDropdown()
        {
            //return _context.ApplicationUsers.Select(x => new SelectListItem()
            //{
            //    Text = x.FullName,
            //    Value = x.Id.ToString()
            //});

            var owners = (from user in _context.ApplicationUsers
                          join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                          join role in _context.Roles on userRoles.RoleId equals role.Id
                          where role.Id == "f0cb917e-cdde-4ba1-925e-64680a57ef5b"
                          select new
                          {
                              VeterinarianId = user.Id,
                              FirstName = user.FirstName,
                              LastName = user.LastName
                          });

            return owners.Select(x => new SelectListItem()
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.VeterinarianId.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetVeterinariansForDropdown()
        {
            var veterinarians = (from user in _context.ApplicationUsers
                          join userRoles in _context.UserRoles on user.Id equals userRoles.UserId
                          join role in _context.Roles on userRoles.RoleId equals role.Id
                          where role.Id == "8847e7c6-0c71-4922-9276-dc3e748090cc"
                          select new
                          {
                              OwnerId = user.Id,
                              FirstName = user.FirstName,
                              LastName = user.LastName
                          }).ToList();

            return veterinarians.Select(x => new SelectListItem()
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.OwnerId.ToString()
            });
        }

    }
}
