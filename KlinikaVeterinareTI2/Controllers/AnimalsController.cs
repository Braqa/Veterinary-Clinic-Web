using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.Utilities;
using KlinikaVeterinareTI2.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KlinikaVeterinareTI2.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        [BindProperty]
        public AnimalViewModel AnimalVM { get; set; }

        public AnimalsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(StaticDetails.User))
            {
                return View(unitOfWork.Animal.GetAll(filter: a => a.UserId == claims.Value, includeProperties: "Race,Family,User"));
            }
            else
            {
                return View(unitOfWork.Animal.GetAll(includeProperties: "Race,Family,User"));
            }

        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            AnimalVM = new AnimalViewModel
            {
                Animal = new Animal(),
                FamilyList = unitOfWork.Family.GetFamilyListForDropdown(),
                RaceList = unitOfWork.Race.GetRaceListForDropdown(),
                OwnerList = unitOfWork.User.GetOwnersForDropdown(),
            };

            if (id != null)
            {
                AnimalVM.Animal = unitOfWork.Animal.Get(id.GetValueOrDefault());
            }

            return View(AnimalVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (!ModelState.IsValid)
            {
                // per mos me dal error qe dropdown jon te pa mushne, 
                // nese para se mu mush dropdown modelstate nuk ka qene valid
                AnimalVM.FamilyList = unitOfWork.Family.GetFamilyListForDropdown();
                AnimalVM.RaceList = unitOfWork.Race.GetRaceListForDropdown();
                AnimalVM.OwnerList = unitOfWork.User.GetOwnersForDropdown();
                return View(AnimalVM);
            }

            string webRootPath = hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (AnimalVM.Animal.Id == 0)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\animals");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    AnimalVM.Animal.ImageUrl = @"\images\animals\" + fileName + extension;

                    AnimalVM.Animal.InsertBy = User.Identity.Name;
                    AnimalVM.Animal.InsertDate = DateTime.Now;
                    AnimalVM.Animal.IsDeleted = false;
                    unitOfWork.Animal.Add(AnimalVM.Animal);
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
                    // Edit Animal
                    var animalFromDb = unitOfWork.Animal.Get(AnimalVM.Animal.Id);

                    if (files.Count > 0)
                    {
                        // Nese ka bo upload file te ri
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\animals");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        // delete original file
                        var imagePath = Path.Combine(webRootPath, animalFromDb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        // upload new file
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        AnimalVM.Animal.ImageUrl = @"\images\animals\" + fileName + extension_new;
                    }
                    else
                    {
                        // nese nuk ka bo upload file te ri
                        AnimalVM.Animal.ImageUrl = animalFromDb.ImageUrl;
                    }

                    AnimalVM.Animal.LUB = User.Identity.Name;
                    AnimalVM.Animal.LUD = DateTime.Now;
                    unitOfWork.Animal.Update(AnimalVM.Animal);
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
            var animal = unitOfWork.Animal.Get(id);
            if (animal is null)
            {
                TempData["alert"] = StaticDetails.NotFoundMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        [HttpPost]
        public IActionResult DeleteAnimal(int id)
        {
            try
            {
                var animal = unitOfWork.Animal.Get(id);

                string webRootPath = hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, animal.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

                if (animal is null)
                {
                    TempData["alert"] = StaticDetails.NotFoundMessage;
                    TempData["aType"] = "danger";
                    return RedirectToAction(nameof(Index));
                }

                //animal.IsDeleted = true;
                unitOfWork.Animal.Remove(animal);
                unitOfWork.Save();

                TempData["alert"] = StaticDetails.DataDeleted;
                TempData["aType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["alert"] = StaticDetails.ErrorMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}
