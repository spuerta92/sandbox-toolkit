using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using toolbox.Enums;
using toolbox.Extensions;
using toolbox.Models;
using toolbox.Records;

static class Program
{
    /// <summary>
    /// ADO.NET Review + SQL Authentication 
    /// </summary>
    static void Main()
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCompany; UID=mycompanyuser; PWD=HelloWorld2026!; Integrated Security=false; Encrypt=true; Trusted_Connection=true;";
        //const string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MyCompany; Id=mycompanyuser; Password=HelloWorld2026!; Integrated Security=false; Encrypt=true; Trusted_Connection=true;";

        const string queryString = @$"SELECT * FROM dbo.Employees";

        var employees = new List<EmployeeDto>();
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
                            ProjectId = reader.GetInt16(reader.GetOrdinal("ProjectId")),
                            StartDate = reader.IsDBNull(reader.GetOrdinal("StartDate")) ? null : reader.GetDateTime(reader.GetOrdinal("StartDate")),
                            TerminationDate = reader.IsDBNull(reader.GetOrdinal("TerminationDate")) ? null : reader.GetDateTime(reader.GetOrdinal("TerminationDate"))
                        };
                        employees.Add(employee.AsEmployeeDto());
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
        Console.Write(JsonConvert.SerializeObject(employees.Where(e => e.DepartmentId == (byte)Departments.InvestmentResearch), Formatting.Indented));
    }
}