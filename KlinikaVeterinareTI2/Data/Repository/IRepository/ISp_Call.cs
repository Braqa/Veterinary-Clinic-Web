using System;
using System.Collections.Generic;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.ViewModels;

namespace KlinikaVeterinareTI2.Data.Repository.IRepository
{
    public interface ISp_Call : IDisposable
    {
        //IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);
        //void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);
        //T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param = null);

        List<StatisticsVaccines> ListVaccines(DateTime fromDate, DateTime toDate);
        List<StatisticsAnimalRace> ListAnimalRace(DateTime fromDate, DateTime toDate);
        List<StatisticsAnimalFamily> ListAnimalFamily(DateTime fromDate, DateTime toDate);
        List<StatisticsAnimalDiagnosis> ListAnimalFDiagnosis(DateTime fromDate, DateTime toDate);
        List<StatisticsVisits> ListStatisticsVisits(DateTime fromDate, DateTime toDate);
        List<Visit> FilterVisits(StatisticsViewModel model);
    }
}
