using System;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using KlinikaVeterinareTI2.Models;
using System.Collections.Generic;
using KlinikaVeterinareTI2.Data;
using Microsoft.EntityFrameworkCore;

namespace KlinikaVeterinareTI2.Dal
{
    public class VaccinesDB
    {
        private readonly ApplicationDbContext _context;
        private readonly string connectionString = "Server=PLEURAT;Database=KlinikaVeterinareTI2;Trusted_Connection=True;MultipleActiveResultSets=true";

        public VaccinesDB()
        {

        }
        public VaccinesDB(ApplicationDbContext context)
        {
            _context = context;
            connectionString = _context.Database.GetDbConnection().ConnectionString;
        }

        public Vaccine GetItem(int id)
        {
            Vaccine myVaccines = null;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_VaccinesSelectSingleItem", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@id", id);

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            myVaccines = FillDataRecord(myReader);
                        }
                        myReader.Close();
                    }
                    myConnection.Close();
                }
                return myVaccines;
            }
        }

        public DataTable GetDataTableList()
        {
            DataTable tempTable = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_VaccinesSelectList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        tempTable.Load(myReader);
                        myReader.Close();
                    }
                }
            }
            return tempTable;
        }

        public List<Vaccine> GetList()
        {
            List<Vaccine> tempList = new List<Vaccine>();
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_VaccinesSelectList", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myConnection.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                tempList.Add(FillDataRecord(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
            }
            return tempList;
        }

        //public static int SelectCountForGetList()
        //{
        //    using (SqlConnection myConnection = new SqlConnection(AppConfiguration.ConnectionString))
        //    {
        //        using (SqlCommand myCommand = new SqlCommand("spVaccinesSelectList", myConnection))
        //        {
        //            myCommand.CommandType = CommandType.StoredProcedure;
        //            DbParameter idParam = myCommand.CreateParameter();
        //            idParam.DbType = DbType.Int32;
        //            idParam.Direction = ParameterDirection.InputOutput;
        //            idParam.ParameterName = "@recordCount";
        //            idParam.Value = 0;
        //            myCommand.Parameters.Add(idParam);
        //            myConnection.Open();
        //            myCommand.ExecuteNonQuery();
        //            myConnection.Close();
        //            return (int)myCommand.Parameters["@recordCount"].Value;
        //        }
        //    }
        //}

        
        public int Save(Vaccine myVaccines)
        { 
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_VaccinesInsertUpdateSingleItem", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    if (myVaccines.Id == -1 || myVaccines.Id == 0)
                    {
                        myCommand.Parameters.AddWithValue("@id", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@id", myVaccines.Id);
                    }
                    if (string.IsNullOrEmpty(myVaccines.SerialNo))
                    {
                        myCommand.Parameters.AddWithValue("@serialNo", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@serialNo", myVaccines.SerialNo);
                    }
                    if (string.IsNullOrEmpty(myVaccines.Name))
                    {
                        myCommand.Parameters.AddWithValue("@name", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@name", myVaccines.Name);
                    }
                    if (string.IsNullOrEmpty(myVaccines.Description))
                    {
                        myCommand.Parameters.AddWithValue("@description", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@description", myVaccines.Description);
                    }
                    if (string.IsNullOrEmpty(myVaccines.InsertBy))
                    {
                        myCommand.Parameters.AddWithValue("@insertBy", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@insertBy", myVaccines.InsertBy);
                    }
                    if (myVaccines.InsertDate == null)
                    {
                        myCommand.Parameters.AddWithValue("@insertDate", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@insertDate", myVaccines.InsertDate);
                    }
                    if (string.IsNullOrEmpty(myVaccines.LUB))
                    {
                        myCommand.Parameters.AddWithValue("@lUB", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@lUB", myVaccines.LUB);
                    }
                    if (myVaccines.LUD == null)
                    {
                        myCommand.Parameters.AddWithValue("@lUD", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@lUD", myVaccines.LUD);
                    }
                    if (myVaccines.LUN == null)
                    {
                        myCommand.Parameters.AddWithValue("@lUN", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@lUN", myVaccines.LUN);
                    }
                    if (myVaccines.IsDeleted == null)
                    {
                        myCommand.Parameters.AddWithValue("@isDeleted", DBNull.Value);
                    }
                    else
                    {
                        myCommand.Parameters.AddWithValue("@isDeleted", myVaccines.IsDeleted);
                    }

                    DbParameter returnValue;
                    returnValue = myCommand.CreateParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    myCommand.Parameters.Add(returnValue);

                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    result = Convert.ToInt32(returnValue.Value);
                    myConnection.Close();
                }
            }
            return result;
        }

        public bool Delete(Vaccine myVaccines)
        {
            int result = 0;
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("usp_VaccinesDeleteSingleItem", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;

                    myCommand.Parameters.AddWithValue("@Id", myVaccines.Id);
                    myConnection.Open();
                    result = myCommand.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
            return result > 0;
        }


        private static Vaccine FillDataRecord(IDataRecord myDataRecord)
        {
            Vaccine myVaccines = new Vaccine();
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Id")))
            {
                myVaccines.Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("Id"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("SerialNo")))
            {
                myVaccines.SerialNo = myDataRecord.GetString(myDataRecord.GetOrdinal("SerialNo"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Name")))
            {
                myVaccines.Name = myDataRecord.GetString(myDataRecord.GetOrdinal("Name"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Description")))
            {
                myVaccines.Description = myDataRecord.GetString(myDataRecord.GetOrdinal("Description"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("InsertBy")))
            {
                myVaccines.InsertBy = myDataRecord.GetString(myDataRecord.GetOrdinal("InsertBy"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("InsertDate")))
            {
                myVaccines.InsertDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("InsertDate"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LUB")))
            {
                myVaccines.LUB = myDataRecord.GetString(myDataRecord.GetOrdinal("LUB"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LUD")))
            {
                myVaccines.LUD = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("LUD"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("LUN")))
            {
                myVaccines.LUN = myDataRecord.GetInt32(myDataRecord.GetOrdinal("LUN"));
            }
            if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("IsDeleted")))
            {
                myVaccines.IsDeleted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("IsDeleted"));
            }
            return myVaccines;
        }
    }
}
