using System;
using KlinikaVeterinareTI2.Dal;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KlinikaVeterinareTI2.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VaccinesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //VaccinesDB db = new VaccinesDB();
        // GET: VaccinesController
        public IActionResult Index()
        {
            return View(_unitOfWork.Vaccine.GetAll());
        }

        // GET: VaccinesController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Vaccine vaccine = new Vaccine();

            if (id is null)
            {
                return View(vaccine);
            }

            //vaccine = db.GetItem(id.GetValueOrDefault());
            //vaccine.InsertBy = User.Identity.Name;
            //vaccine.InsertDate = DateTime.Now;

            vaccine = _unitOfWork.Vaccine.Get(id.GetValueOrDefault());

            if (vaccine is null)
            {
                TempData["alert"] = StaticDetails.NotFoundMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index));
            }

            return View(vaccine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Vaccine vaccine)
        {
            if (!ModelState.IsValid)
                return View(vaccine);

            if (vaccine.Id == 0)
            {
                try
                {
                    vaccine.InsertBy = User.Identity.Name;
                    vaccine.InsertDate = DateTime.Now;
                    vaccine.IsDeleted = false;
                    _unitOfWork.Vaccine.Add(vaccine);
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
                    vaccine.LUB = User.Identity.Name;
                    vaccine.LUD = DateTime.Now;
                    _unitOfWork.Vaccine.Update(vaccine);
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

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vaccine = _unitOfWork.Vaccine.Get(id);
            if (vaccine is null)
            {
                TempData["alert"] = StaticDetails.NotFoundMessage;
                TempData["aType"] = "danger";
                return RedirectToAction(nameof(Index));
            }
            return View(vaccine);
        }

        [HttpPost]
        public IActionResult DeleteVaccine(int id)
        {
            try
            {
                var vaccine = _unitOfWork.Vaccine.Get(id);

                if (vaccine is null)
                {
                    TempData["alert"] = StaticDetails.NotFoundMessage;
                    TempData["aType"] = "danger";
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Vaccine.Remove(vaccine);
                _unitOfWork.Save();

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
