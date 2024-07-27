using System;
using System.Diagnostics;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KlinikaVeterinareTI2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

            return LocalRedirect(returnUrl);
        }

        public IActionResult Statistics()
        {
            int year = DateTime.Now.Year;
            DateTime from = new DateTime(year, 1, 1);
            StatisticsViewModel model = new StatisticsViewModel();
            model.StatisticsNumberOfVaccines = unitOfWork.Sp_Call.ListVaccines(from, DateTime.Now);
            model.StatisticsAnimalRace = unitOfWork.Sp_Call.ListAnimalRace(from, DateTime.Now);
            model.StatisticsAnimalFamily = unitOfWork.Sp_Call.ListAnimalFamily(from, DateTime.Now);
            model.StatisticsAnimalDiagnosis = unitOfWork.Sp_Call.ListAnimalFDiagnosis(from, DateTime.Now);
            model.StatisticsVisits = unitOfWork.Sp_Call.ListStatisticsVisits(from, DateTime.Now);
            return View(model);
        }

        [HttpPost]
        public IActionResult Statistics(StatisticsViewModel model, DateTime? fromDate = null, DateTime? toDate = null)
        {
            TempData["fromDate"] = fromDate;
            TempData["toDate"] = toDate;
            int year = DateTime.Now.Year;
            fromDate = fromDate ?? new DateTime(year, 1, 1);
            toDate = toDate ?? DateTime.Now;
            model.StatisticsNumberOfVaccines = unitOfWork.Sp_Call.ListVaccines(fromDate.GetValueOrDefault(), toDate.GetValueOrDefault());
            model.StatisticsAnimalRace = unitOfWork.Sp_Call.ListAnimalRace(fromDate.GetValueOrDefault(), toDate.GetValueOrDefault());
            model.StatisticsAnimalFamily = unitOfWork.Sp_Call.ListAnimalFamily(fromDate.GetValueOrDefault(), toDate.GetValueOrDefault());
            model.StatisticsAnimalDiagnosis = unitOfWork.Sp_Call.ListAnimalFDiagnosis(fromDate.GetValueOrDefault(), toDate.GetValueOrDefault());
            model.StatisticsVisits = unitOfWork.Sp_Call.ListStatisticsVisits(fromDate.GetValueOrDefault(), toDate.GetValueOrDefault());
            return View(model);
        }

        public IActionResult Filter()
        {
            StatisticsViewModel statisticsVM = new StatisticsViewModel
            {
                YearFromSelectList = unitOfWork.Visit.GetVisitDatesForDropdown(),
                YearToSelectList = unitOfWork.Visit.GetVisitDatesForDropdown(),
                AnimalFamilySelectList = unitOfWork.Family.GetFamilyListForDropdown(),
                AnimalRaceSelectList = unitOfWork.Race.GetRaceListForDropdown(),
                AnimalGenderSelectList = unitOfWork.Animal.GetAnimalGenderListForDropdown(),
                VeterinarySelectList = unitOfWork.User.GetVeterinariansForDropdown(),
            };

            return View(statisticsVM);
        }

        [HttpPost]
        public IActionResult Filter(StatisticsViewModel model)
        {
            model.YearFromSelectList = unitOfWork.Visit.GetVisitDatesForDropdown();
            model.YearToSelectList = unitOfWork.Visit.GetVisitDatesForDropdown();
            model.AnimalFamilySelectList = unitOfWork.Family.GetFamilyListForDropdown();
            model.AnimalRaceSelectList = unitOfWork.Race.GetRaceListForDropdown();
            model.AnimalGenderSelectList = unitOfWork.Animal.GetAnimalGenderListForDropdown();
            model.VeterinarySelectList = unitOfWork.User.GetVeterinariansForDropdown();
            model.FilteredVisits = unitOfWork.Sp_Call.FilterVisits(model);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
