using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Sp0908.Models;

namespace Sp0908.DAL
{
    public class Employee_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        public List<Employee> GetAllEmployees()
        {
            List<Employee> EmployeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Sp_employee_Select", connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();
                SqlDA.Fill(dtEmployees);

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    EmployeeList.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Department = dr["Department"].ToString(),
                        Sallary = Convert.ToInt32(dr["Sallary"]),
                        City = dr["City"].ToString(),
                        Country = dr["Country"].ToString(),
                    });
                }

            }

            return EmployeeList;
        }

        public bool InsertEmployees(Employee employee)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Sp_employee_Add1", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("FirstName", employee.FirstName);
                command.Parameters.AddWithValue("LastName", employee.LastName);
                command.Parameters.AddWithValue("Age", employee.Age);
                command.Parameters.AddWithValue("Department", employee.Department);
                command.Parameters.AddWithValue("Sallary", employee.Sallary);
                command.Parameters.AddWithValue("City", employee.City);
                command.Parameters.AddWithValue("Country", employee.Country);
                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if(id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Employee> GetEmployeeById(int Id)
        {
            List<Employee> EmployeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Sp_Employee_Find1";
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataAdapter SqlDA = new SqlDataAdapter(command);
                DataTable dtEmployees = new DataTable();

                connection.Open();
                SqlDA.Fill(dtEmployees);
                connection.Close();

                foreach (DataRow dr in dtEmployees.Rows)
                {
                    EmployeeList.Add(new Employee
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Department = dr["Department"].ToString(),
                        Sallary = Convert.ToInt32(dr["Sallary"]),
                        City = dr["City"].ToString(),
                        Country = dr["Country"].ToString(),
                    });
                }

            }

            return EmployeeList;
        }

        public bool UpdateEmployee(Employee employee)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Sp_employee_Update1", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Id", employee.Id);
                command.Parameters.AddWithValue("FirstName", employee.FirstName);
                command.Parameters.AddWithValue("LastName", employee.LastName);
                command.Parameters.AddWithValue("Age", employee.Age);
                command.Parameters.AddWithValue("Department", employee.Department);
                command.Parameters.AddWithValue("Sallary", employee.Sallary);
                command.Parameters.AddWithValue("City", employee.City);
                command.Parameters.AddWithValue("Country", employee.Country);
                connection.Open();
                i = command.ExecuteNonQuery();
                connection.Close();
            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string DeleteEmployee (int Id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("Sp_employee_Delete",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }
                return result;
        }

    }
}