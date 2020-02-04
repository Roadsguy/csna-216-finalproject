using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FinalProject
{
	public class LouisDataTier
	{
		static String connString = ConfigurationManager.ConnectionStrings["ConnLouis"].ConnectionString;
		static SqlConnection myConn = new SqlConnection(connString);
		static SqlCommand cmdString = new SqlCommand();

		public bool AddPatient(string strPatientID, string strFName, string strLName, string strMInit, char charGender, string strDateOfBirth, string strAcctBalance,
			string strInsuranceCo, string strAddrLine1, string strAddrLine2, string strAddrCity, string strAddrState, string strAddrZip, string strEmail1,
			string strEmail2, string strHomePhoneNo, string strCellPhoneNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "AddPatient";

				// Define input parameters
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@mInit", SqlDbType.Char, 1).Value = strMInit;
				cmdString.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = strDateOfBirth;
				cmdString.Parameters.Add("@gender", SqlDbType.Char, 1).Value = charGender;
				cmdString.Parameters.Add("@addrLine1", SqlDbType.VarChar, 60).Value = strAddrLine1;
				cmdString.Parameters.Add("@addrLine2", SqlDbType.VarChar, 60).Value = strAddrLine2;
				cmdString.Parameters.Add("@addrCity", SqlDbType.VarChar, 60).Value = strAddrCity;
				cmdString.Parameters.Add("@addrState", SqlDbType.Char, 2).Value = strAddrState;
				cmdString.Parameters.Add("@addrZIP", SqlDbType.VarChar, 5).Value = strAddrZip;
				cmdString.Parameters.Add("@email1", SqlDbType.VarChar, 60).Value = strEmail1;
				cmdString.Parameters.Add("@email2", SqlDbType.VarChar, 60).Value = strEmail2;
				cmdString.Parameters.Add("@homePhoneNo", SqlDbType.VarChar, 12).Value = strHomePhoneNo;
				cmdString.Parameters.Add("@cellPhoneNo", SqlDbType.VarChar, 12).Value = strCellPhoneNo;
				cmdString.Parameters.Add(new SqlParameter("@acctBalance", SqlDbType.Decimal) { Precision = 7, Scale = 2 }).Value = strAcctBalance;
				cmdString.Parameters.Add("@insuranceCo", SqlDbType.VarChar, 60).Value = strInsuranceCo;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet SearchPatients(string strPatientID, string strLName, string strFName)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "SearchPatients";

				// Define input parameters
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;

				// Adapter and dataset
				SqlDataAdapter aAdapter = new SqlDataAdapter();
				aAdapter.SelectCommand = cmdString;
				DataSet aDataSet = new DataSet();

				// Fill adapater
				aAdapter.Fill(aDataSet);

				// Return dataSet
				return aDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet GetPatient(string strPatientID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "GetPatientForUpdate";

				// Define input parameters
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;

				// Adapter and dataset
				SqlDataAdapter bAdapter = new SqlDataAdapter();
				bAdapter.SelectCommand = cmdString;
				DataSet bDataSet = new DataSet();

				// Fill adapater
				bAdapter.Fill(bDataSet);

				// Return dataSet
				return bDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool UpdatePatient(string strPatientID, string strFName, string strLName, string strMInit, char charGender, string strDateOfBirth, string strAcctBalance,
			string strInsuranceCo, string strAddrLine1, string strAddrLine2, string strAddrCity, string strAddrState, string strAddrZip, string strEmail1,
			string strEmail2, string strHomePhoneNo, string strCellPhoneNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "UpdatePatients";

				// Define input parameters
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@mInit", SqlDbType.Char, 1).Value = strMInit;
				cmdString.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = strDateOfBirth;
				cmdString.Parameters.Add("@gender", SqlDbType.Char, 1).Value = charGender;
				cmdString.Parameters.Add("@addrLine1", SqlDbType.VarChar, 60).Value = strAddrLine1;
				cmdString.Parameters.Add("@addrLine2", SqlDbType.VarChar, 60).Value = strAddrLine2;
				cmdString.Parameters.Add("@addrCity", SqlDbType.VarChar, 60).Value = strAddrCity;
				cmdString.Parameters.Add("@addrState", SqlDbType.Char, 2).Value = strAddrState;
				cmdString.Parameters.Add("@addrZIP", SqlDbType.VarChar, 5).Value = strAddrZip;
				cmdString.Parameters.Add("@email1", SqlDbType.VarChar, 60).Value = strEmail1;
				cmdString.Parameters.Add("@email2", SqlDbType.VarChar, 60).Value = strEmail2;
				cmdString.Parameters.Add("@homePhoneNo", SqlDbType.VarChar, 12).Value = strHomePhoneNo;
				cmdString.Parameters.Add("@cellPhoneNo", SqlDbType.VarChar, 12).Value = strCellPhoneNo;
				cmdString.Parameters.Add(new SqlParameter("@acctBalance", SqlDbType.Decimal) { Precision = 7, Scale = 2 }).Value = strAcctBalance;
				cmdString.Parameters.Add("@insuranceCo", SqlDbType.VarChar, 60).Value = strInsuranceCo;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool DeletePatient(string strPatientID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "DeletePatient";

				// Define input parameter
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool AddPhysician(string strPhysicianID, string strFName, string strLName, string strMInit, char charGender, string strDateOfBirth, string strEmployer,
			string strSpecialty1, string strSpecialty2, string strSpecialty3, string strAddrLine1, string strAddrLine2, string strAddrCity, string strAddrState,
			string strAddrZip, string strWorkEmail, string strPersonalEmail, string strWorkPhoneNo, string strHomePhoneNo, string strCellPhoneNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "AddPhysician";

				// Define input parameters
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@mInit", SqlDbType.Char, 1).Value = strMInit;
				cmdString.Parameters.Add("@gender", SqlDbType.Char, 1).Value = charGender;
				cmdString.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = strDateOfBirth;
				cmdString.Parameters.Add("@employer", SqlDbType.VarChar, 60).Value = strEmployer;
				cmdString.Parameters.Add("@specialty1", SqlDbType.VarChar, 30).Value = strSpecialty1;
				cmdString.Parameters.Add("@specialty2", SqlDbType.VarChar, 30).Value = strSpecialty2;
				cmdString.Parameters.Add("@specialty3", SqlDbType.VarChar, 30).Value = strSpecialty3;
				cmdString.Parameters.Add("@addrLine1", SqlDbType.VarChar, 60).Value = strAddrLine1;
				cmdString.Parameters.Add("@addrLine2", SqlDbType.VarChar, 60).Value = strAddrLine2;
				cmdString.Parameters.Add("@addrCity", SqlDbType.VarChar, 60).Value = strAddrCity;
				cmdString.Parameters.Add("@addrState", SqlDbType.Char, 2).Value = strAddrState;
				cmdString.Parameters.Add("@addrZIP", SqlDbType.VarChar, 5).Value = strAddrZip;
				cmdString.Parameters.Add("@workEmail", SqlDbType.VarChar, 60).Value = strWorkEmail;
				cmdString.Parameters.Add("@personalEmail", SqlDbType.VarChar, 60).Value = strPersonalEmail;
				cmdString.Parameters.Add("@workPhoneNo", SqlDbType.VarChar, 12).Value = strWorkPhoneNo;
				cmdString.Parameters.Add("@homePhoneNo", SqlDbType.VarChar, 12).Value = strHomePhoneNo;
				cmdString.Parameters.Add("@cellPhoneNo", SqlDbType.VarChar, 12).Value = strCellPhoneNo;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet SearchPhysicians(string strPhysicianID, string strFName, string strLName, string strEmployer)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "SearchPhysicians";

				// Define input parameters
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@employer", SqlDbType.VarChar, 60).Value = strEmployer;

				// Adapter and dataset
				SqlDataAdapter aAdapter = new SqlDataAdapter();
				aAdapter.SelectCommand = cmdString;
				DataSet aDataSet = new DataSet();

				// Fill adapater
				aAdapter.Fill(aDataSet);

				// Return dataSet
				return aDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet GetPhysician(string strPhysicianID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "GetPhysicianForUpdate";

				// Define input parameters
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;

				// Adapter and dataset
				SqlDataAdapter bAdapter = new SqlDataAdapter();
				bAdapter.SelectCommand = cmdString;
				DataSet bDataSet = new DataSet();

				// Fill adapater
				bAdapter.Fill(bDataSet);

				// Return dataSet
				return bDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool UpdatePhysician(string strPhysicianID, string strFName, string strLName, string strMInit, char charGender, string strDateOfBirth, string strEmployer,
			string strSpecialty1, string strSpecialty2, string strSpecialty3, string strAddrLine1, string strAddrLine2, string strAddrCity, string strAddrState,
			string strAddrZip, string strWorkEmail, string strPersonalEmail, string strWorkPhoneNo, string strHomePhoneNo, string strCellPhoneNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "UpdatePhysicians";

				// Define input parameters
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;
				cmdString.Parameters.Add("@fName", SqlDbType.VarChar, 50).Value = strFName;
				cmdString.Parameters.Add("@lName", SqlDbType.VarChar, 50).Value = strLName;
				cmdString.Parameters.Add("@mInit", SqlDbType.Char, 1).Value = strMInit;
				cmdString.Parameters.Add("@gender", SqlDbType.Char, 1).Value = charGender;
				cmdString.Parameters.Add("@dateOfBirth", SqlDbType.Date).Value = strDateOfBirth;
				cmdString.Parameters.Add("@employer", SqlDbType.VarChar, 60).Value = strEmployer;
				cmdString.Parameters.Add("@specialty1", SqlDbType.VarChar, 30).Value = strSpecialty1;
				cmdString.Parameters.Add("@specialty2", SqlDbType.VarChar, 30).Value = strSpecialty2;
				cmdString.Parameters.Add("@specialty3", SqlDbType.VarChar, 30).Value = strSpecialty3;
				cmdString.Parameters.Add("@addrLine1", SqlDbType.VarChar, 60).Value = strAddrLine1;
				cmdString.Parameters.Add("@addrLine2", SqlDbType.VarChar, 60).Value = strAddrLine2;
				cmdString.Parameters.Add("@addrCity", SqlDbType.VarChar, 60).Value = strAddrCity;
				cmdString.Parameters.Add("@addrState", SqlDbType.Char, 2).Value = strAddrState;
				cmdString.Parameters.Add("@addrZIP", SqlDbType.VarChar, 5).Value = strAddrZip;
				cmdString.Parameters.Add("@workEmail", SqlDbType.VarChar, 60).Value = strWorkEmail;
				cmdString.Parameters.Add("@personalEmail", SqlDbType.VarChar, 60).Value = strPersonalEmail;
				cmdString.Parameters.Add("@workPhoneNo", SqlDbType.VarChar, 12).Value = strWorkPhoneNo;
				cmdString.Parameters.Add("@homePhoneNo", SqlDbType.VarChar, 12).Value = strHomePhoneNo;
				cmdString.Parameters.Add("@cellPhoneNo", SqlDbType.VarChar, 12).Value = strCellPhoneNo;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool DeletePhysician(string strPhysicianID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "DeletePhysician";

				// Define input parameter
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool AddDrug(string strDrugID, string strDrugName, string strDrugDesc, string strMethodOfAdmin)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "AddDrug";

				// Define input parameters
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@drugName", SqlDbType.VarChar, 50).Value = strDrugName;
				cmdString.Parameters.Add("@drugDesc", SqlDbType.VarChar, 50).Value = strDrugDesc;
				cmdString.Parameters.Add("@methodOfAdmin", SqlDbType.VarChar, 50).Value = strMethodOfAdmin;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet SearchDrugs(string strDrugID, string strDrugName, string strDrugDesc)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "SearchDrugs";

				// Define input parameters
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@drugName", SqlDbType.VarChar, 50).Value = strDrugName;
				cmdString.Parameters.Add("@drugDesc", SqlDbType.VarChar, 50).Value = strDrugDesc;

				// Adapter and dataset
				SqlDataAdapter aAdapter = new SqlDataAdapter();
				aAdapter.SelectCommand = cmdString;
				DataSet aDataSet = new DataSet();

				// Fill adapater
				aAdapter.Fill(aDataSet);

				// Return dataSet
				return aDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet GetDrug(string strDrugID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "GetDrugForUpdate";

				// Define input parameters
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;

				// Adapter and dataset
				SqlDataAdapter bAdapter = new SqlDataAdapter();
				bAdapter.SelectCommand = cmdString;
				DataSet bDataSet = new DataSet();

				// Fill adapater
				bAdapter.Fill(bDataSet);

				// Return dataSet
				return bDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool UpdateDrug(string strDrugID, string strDrugName, string strDrugDesc, string strMethodOfAdmin)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "UpdateDrugs";

				// Define input parameters
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@drugName", SqlDbType.VarChar, 50).Value = strDrugName;
				cmdString.Parameters.Add("@drugDesc", SqlDbType.VarChar, 50).Value = strDrugDesc;
				cmdString.Parameters.Add("@methodOfAdmin", SqlDbType.VarChar, 50).Value = strMethodOfAdmin;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool DeleteDrug(string strDrugID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "DeleteDrug";

				// Define input parameter
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool AddPrescription(string strRxNo, string strDrugID, string strPatientID, string strPhysicianID, string strDosage,
			string strFrequency, string strStartDate, string strFinishDate, string strRefills, string strCost)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "AddPrescription";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;
				cmdString.Parameters.Add("@dosage", SqlDbType.VarChar, 50).Value = strDosage;
				cmdString.Parameters.Add("@frequency", SqlDbType.VarChar, 50).Value = strFrequency;
				cmdString.Parameters.Add("@startDate", SqlDbType.Date).Value = strStartDate;
				cmdString.Parameters.Add("@finishDate", SqlDbType.Date).Value = strFinishDate;
				cmdString.Parameters.Add("@refillsLeft", SqlDbType.Int).Value = int.Parse(strRefills);
				cmdString.Parameters.Add("@refillsGiven", SqlDbType.Int).Value = int.Parse(strRefills);
				cmdString.Parameters.Add(new SqlParameter("@cost", SqlDbType.Decimal) { Precision = 9, Scale = 2 }).Value = strCost;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet SearchPrescriptions(string strRxNo, string strPatientID, string strDrugID, string strPhysicianID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "SearchPrescription";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;

				// Adapter and dataset
				SqlDataAdapter aAdapter = new SqlDataAdapter();
				aAdapter.SelectCommand = cmdString;
				DataSet aDataSet = new DataSet();

				// Fill adapater
				aAdapter.Fill(aDataSet);

				// Return dataSet
				return aDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet GetPrescription(string strRxNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "GetPrescriptionForUpdate";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;

				// Adapter and dataset
				SqlDataAdapter bAdapter = new SqlDataAdapter();
				bAdapter.SelectCommand = cmdString;
				DataSet bDataSet = new DataSet();

				// Fill adapater
				bAdapter.Fill(bDataSet);

				// Return dataSet
				return bDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool UpdatePrescription(string strRxNo, string strDrugID, string strPatientID, string strPhysicianID, string strDosage,
			string strFrequency, string strStartDate, string strFinishDate, string strRefillsLeft, string strCost, string strRefillsGiven)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "UpdatePrescriptions";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;
				cmdString.Parameters.Add("@dosage", SqlDbType.VarChar, 50).Value = strDosage;
				cmdString.Parameters.Add("@frequency", SqlDbType.VarChar, 50).Value = strFrequency;
				cmdString.Parameters.Add("@startDate", SqlDbType.Date).Value = strStartDate;
				cmdString.Parameters.Add("@finishDate", SqlDbType.Date).Value = strFinishDate;
				cmdString.Parameters.Add("@refillsLeft", SqlDbType.Int).Value = int.Parse(strRefillsLeft);
				cmdString.Parameters.Add(new SqlParameter("@cost", SqlDbType.Decimal) { Precision = 9, Scale = 2 }).Value = strCost;
				cmdString.Parameters.Add("@refillsGiven", SqlDbType.Int).Value = int.Parse(strRefillsGiven);

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool DeletePrescription(string strRxNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "DeletePrescription";

				// Define input parameter
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool AddRefill(string strRxNo, string strRefillDateTime)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "AddRefill";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@refillDateTime", SqlDbType.DateTime).Value = strRefillDateTime;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet SearchRefills(string strRxNo, string strPatientID, string strDrugID, string strPhysicianID)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "SearchRefills";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@patientID", SqlDbType.VarChar, 8).Value = strPatientID;
				cmdString.Parameters.Add("@drugID", SqlDbType.VarChar, 8).Value = strDrugID;
				cmdString.Parameters.Add("@physicianID", SqlDbType.VarChar, 8).Value = strPhysicianID;

				// Adapter and dataset
				SqlDataAdapter aAdapter = new SqlDataAdapter();
				aAdapter.SelectCommand = cmdString;
				DataSet aDataSet = new DataSet();

				// Fill adapater
				aAdapter.Fill(aDataSet);

				// Return dataSet
				return aDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public DataSet GetRefill(string strRxNo, string strRefillNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "GetRefillForUpdate";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@refillNo", SqlDbType.Int).Value = int.Parse(strRefillNo);

				// Adapter and dataset
				SqlDataAdapter bAdapter = new SqlDataAdapter();
				bAdapter.SelectCommand = cmdString;
				DataSet bDataSet = new DataSet();

				// Fill adapater
				bAdapter.Fill(bDataSet);

				// Return dataSet
				return bDataSet;
			}
			catch (Exception ex)
			{
				// Throw exception
				throw new ArgumentException(ex.Message);
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool UpdateRefill(string strRxNo, string strRefillNo, string strRefillDateTime)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "UpdateRefills";

				// Define input parameters
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@refillNo", SqlDbType.Int).Value = int.Parse(strRefillNo);
				cmdString.Parameters.Add("@refillDateTime", SqlDbType.DateTime).Value = strRefillDateTime;

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}

		public bool DeleteRefill(string strRxNo, string strRefillNo)
		{
			try
			{
				// Open connection
				myConn.Open();

				// Clear command arguments
				cmdString.Parameters.Clear();

				// SQL command
				cmdString.Connection = myConn;
				cmdString.CommandType = CommandType.StoredProcedure;
				cmdString.CommandTimeout = 1500;
				cmdString.CommandText = "DeleteRefill";

				// Define input parameter
				cmdString.Parameters.Add("@rxNo", SqlDbType.VarChar, 8).Value = strRxNo;
				cmdString.Parameters.Add("@refillNo", SqlDbType.Int).Value = int.Parse(strRefillNo);

				// Execute stored procedure
				cmdString.ExecuteNonQuery();

				// Return success
				return true;
			}
			catch
			{
				// Return failure
				return false;
			}
			finally
			{
				// Close connection
				myConn.Close();
			}
		}
	}
}