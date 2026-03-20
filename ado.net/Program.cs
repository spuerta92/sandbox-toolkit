using Garage.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

static class Program
{
    /// <summary>
    /// ADO.NET Review
    /// </summary>
    static void Main()
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCompany; Integrated Security=true";
        //const string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MyCompany; Trusted_Connection=True";

        const string queryString = @$"SELECT * FROM dbo.Employees";

        var employees = new List<Employee>();
        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(queryString, connection);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee()
                        {
                            EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                            RoleId = reader.GetByte(reader.GetOrdinal("RoleId")),
                            DepartmentId = reader.GetByte(reader.GetOrdinal("DepartmentId")),
                            ProjectId = (ushort)reader.GetInt16(reader.GetOrdinal("ProjectId")),
                            StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? null : reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate")) ? null : reader.GetDateTime(reader.GetOrdinal("TerminationDate"))
                        };
                        employees.Add(employee);
                    }
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // output
        Console.Write(JsonConvert.SerializeObject(employees, Formatting.Indented));
    }
}