using System;
using System.Collections.Generic;
using System.Data;
using KlinikaVeterinareTI2.Data.Repository.IRepository;
using KlinikaVeterinareTI2.Models;
using KlinikaVeterinareTI2.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KlinikaVeterinareTI2.Data.Repository
{
    public class Sp_Call : ISp_Call
    {
        private readonly ApplicationDbContext _context;
        private readonly string connectionString = "Server=PLEURAT;Database=KlinikaVeterinareTI2;Trusted_Connection=True;MultipleActiveResultSets=true";

        public Sp_Call(ApplicationDbContext context)
        {
            _context = context;
            connectionString = _context.Database.GetDbConnection().ConnectionString;
        }

        public List<StatisticsVaccines> ListVaccines(DateTime fromDate, DateTime toDate)
        {
            List<StatisticsVaccines> tempList = new List<StatisticsVaccines>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_StatisticsVaccines", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    myCommand.Parameters.AddWithValue("@toDate", toDate);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new StatisticsVaccines();
                                item.Name = myReader["Name"].ToString();
                                item.Frequency = int.Parse(myReader["VaccineNr"].ToString());

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public List<StatisticsAnimalRace> ListAnimalRace(DateTime fromDate, DateTime toDate)
        {
            List<StatisticsAnimalRace> tempList = new List<StatisticsAnimalRace>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_StatisticsAnimalRace", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    myCommand.Parameters.AddWithValue("@toDate", toDate);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new StatisticsAnimalRace();
                                item.Name = myReader["Name"].ToString();
                                item.Frequency = int.Parse(myReader["AnimalNr"].ToString());

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public List<StatisticsAnimalFamily> ListAnimalFamily(DateTime fromDate, DateTime toDate)
        {
            List<StatisticsAnimalFamily> tempList = new List<StatisticsAnimalFamily>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_StatisticsAnimalFamily", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    myCommand.Parameters.AddWithValue("@toDate", toDate);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new StatisticsAnimalFamily();
                                item.Name = myReader["Name"].ToString();
                                item.Frequency = int.Parse(myReader["AnimalNr"].ToString());

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public List<StatisticsAnimalDiagnosis> ListAnimalFDiagnosis(DateTime fromDate, DateTime toDate)
        {
            List<StatisticsAnimalDiagnosis> tempList = new List<StatisticsAnimalDiagnosis>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_StatisticsAnimalDiagnosis", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    myCommand.Parameters.AddWithValue("@toDate", toDate);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new StatisticsAnimalDiagnosis();
                                item.Name = myReader["Diagnosis"].ToString();
                                item.Frequency = int.Parse(myReader["DiagnosisNr"].ToString());

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public List<StatisticsVisits> ListStatisticsVisits(DateTime fromDate, DateTime toDate)
        {
            List<StatisticsVisits> tempList = new List<StatisticsVisits>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_StatisticsVisitsNumber", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@fromDate", fromDate);
                    myCommand.Parameters.AddWithValue("@toDate", toDate);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new StatisticsVisits();
                                item.Date = int.Parse(myReader["Year"].ToString());
                                item.DateTime = DateTime.Parse(myReader["Date"].ToString());
                                item.Frequency = int.Parse(myReader["NrVisits"].ToString());

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public List<Visit> FilterVisits(StatisticsViewModel model)
        {
            List<Visit> tempList = new List<Visit>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_FilterVisits", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@YearFrom", model.YearFrom);
                    myCommand.Parameters.AddWithValue("@YearTo", model.YearTo);
                    myCommand.Parameters.AddWithValue("@AnimalFamily", model.AnimalFamily);
                    myCommand.Parameters.AddWithValue("@AnimalRace", model.AnimalRace);
                    myCommand.Parameters.AddWithValue("@AnimalGender", model.AnimalGender);
                    myCommand.Parameters.AddWithValue("@Veterinary", model.Veterinary);
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                var item = new Visit();
                                item.Date = DateTime.Parse(myReader["Date"].ToString());

                                item.Animal = new Animal();
                                item.Animal.Name = myReader["Name"].ToString();
                                item.Animal.Gender = myReader["Gender"].ToString();

                                item.Animal.Race = new Race();
                                item.Animal.Race.Name = myReader["Name"].ToString();

                                item.Animal.Family = new Family();
                                item.Animal.Family.Name = myReader["Name"].ToString();

                                item.Veterinarian = new ApplicationUser();
                                item.Veterinarian.FirstName = myReader["FirstName"].ToString();
                                item.Veterinarian.LastName = myReader["LastName"].ToString();

                                item.Owner = new ApplicationUser();
                                item.Owner.FirstName = myReader["FirstName"].ToString();
                                item.Owner.LastName = myReader["LastName"].ToString();

                                tempList.Add(item);
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
