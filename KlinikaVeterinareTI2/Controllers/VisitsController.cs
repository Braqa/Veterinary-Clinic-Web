using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.Utilities;
using KlinikaVeterinareTI2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KlinikaVeterinareTI2.Controllers
{
    public class VisitsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        [BindProperty]
        public VisitViewModel VisitVM { get; set; }

        public VisitsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(StaticDetails.Veterinar))
            {
                return View(unitOfWork.Visit.GetAll(filter: v => v.VeterinarianId == claims.Value, includeProperties: "Animal,Owner,Veterinarian"));
            }
            else
            {
                return View(unitOfWork.Visit.GetAll(includeProperties: "Animal,Owner,Veterinarian"));
            }
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            VisitVM = new VisitViewModel
            {
                Visit = new Visit(),
                AnimalList = unitOfWork.Animal.GetAnimalListForDropdown(),
                VeterinarianList = unitOfWork.User.GetVeterinariansForDropdown(),
                OwnerList = unitOfWork.User.GetOwnersForDropdown(),
            };

            if (id != null)
            {
                VisitVM.Visit = unitOfWork.Visit.Get(id.GetValueOrDefault());
            }

            return View(VisitVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (!ModelState.IsValid)
            {
                VisitVM.AnimalList = unitOfWork.Animal.GetAnimalListForDropdown();
                VisitVM.VeterinarianList = unitOfWork.User.GetVeterinariansForDropdown();
                VisitVM.OwnerList = unitOfWork.User.GetOwnersForDropdown();
                return View(VisitVM);
            }

            if (VisitVM.Visit.Id == 0)
            {
                try
                {
                    VisitVM.Visit.InsertBy = User.Identity.Name;
                    VisitVM.Visit.InsertDate = DateTime.Now;
                    VisitVM.Visit.IsDeleted = false;
                    unitOfWork.Visit.Add(VisitVM.Visit);
                    TempData["alert"] = StaticDetails.DataAdded;
                    TempData["aType"] = "success";
                }
                catch (Exception ex)
                {
                    TempData["alert"] = StaticDetails.ErrorMessage;
                    TempData["aType"] = "danger";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                try
                {
                    var visitFromDb = unitOfWork.Visit.Get(VisitVM.Visit.Id);
                    VisitVM.Visit.LUB = User.Identity.Name;
                    VisitVM.Visit.LUD = DateTime.Now;
                    unitOfWork.Visit.Update(VisitVM.Visit);
                    TempData["alert"] = StaticDetails.DataModified;
                    TempData["aType"] = "success";
                }
                catch (Exception ex)
                {
                    TempData["alert"] = StaticDetails.ErrorMessage;
                    TempData["aType"] = "danger";
                    return RedirectToAction(nameof(Index));
                }
            }

            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var visit = unitOfWork.Visit.Get(id);
            if (visit is null)
            {
                TempData["alert"] = StaticDetails.NotFoundMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index)); 
            }
            return View(visit);
        }

        [HttpPost]
        public IActionResult DeleteVisit(int id)
        {
            try
            {
                var visit = unitOfWork.Visit.Get(id);

                if (visit is null)
                {
                    TempData["alert"] = StaticDetails.NotFoundMessage;
                    TempData["aType"] = "danger";
                    return RedirectToAction(nameof(Index));
                }

                //visit.IsDeleted = true;
                unitOfWork.Visit.Remove(visit);
                unitOfWork.Save();

                TempData["alert"] = StaticDetails.DataDeleted;
                TempData["aType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["alert"] = StaticDetails.ErrorMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index));
            }

        }



    }
}
