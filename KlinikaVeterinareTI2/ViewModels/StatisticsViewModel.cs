using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KlinikaVeterinareTI2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KlinikaVeterinareTI2.ViewModels
{
    public class StatisticsViewModel
    {
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime FromDate { get; set; }
        //public DateTime ToDate { get; set; }

        // Statistics
        public List<StatisticsVaccines> StatisticsNumberOfVaccines { get; set; }
        public List<StatisticsAnimalRace> StatisticsAnimalRace { get; set; }
        public List<StatisticsAnimalFamily> StatisticsAnimalFamily { get; set; }
        public List<StatisticsAnimalDiagnosis> StatisticsAnimalDiagnosis { get; set; }
        public List<StatisticsVisits> StatisticsVisits { get; set; }

        public List<Visit> FilteredVisits { get; set; }


        // Reports
        public int YearFrom { get; set; }
        public IEnumerable<SelectListItem> YearFromSelectList { get; set; }

        public int YearTo { get; set; }
        public IEnumerable<SelectListItem> YearToSelectList { get; set; }

        public int AnimalFamily { get; set; }
        public IEnumerable<SelectListItem> AnimalFamilySelectList { get; set; }

        public int AnimalRace { get; set; }
        public IEnumerable<SelectListItem> AnimalRaceSelectList { get; set; }

        public string AnimalGender { get; set; }
        public IEnumerable<SelectListItem> AnimalGenderSelectList { get; set; }

        public string Veterinary { get; set; }
        public IEnumerable<SelectListItem> VeterinarySelectList { get; set; }

    }

    public class StatisticsVaccines
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
    }

    public class StatisticsAnimalRace
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
    }

    public class StatisticsAnimalFamily
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
    }

    public class StatisticsAnimalDiagnosis
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
    }

    public class StatisticsVisits
    {
        public DateTime DateTime { get; set; }
        public int Date { get; set; }
        public int Frequency { get; set; }
    }
}
