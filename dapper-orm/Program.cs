using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using toolbox.Records;

static class Program {
    static void Main()
    {
        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MyCompany; Integrated Security=true; Trusted_Connection=true;";
        var sql = "SELECT * FROM dbo.Employees";
        var employees = new List<EmployeeDto>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            employees = connection.Query<EmployeeDto>(sql).ToList();
        }

        // output
        Console.Write(JsonConvert.SerializeObject(employees.Take(5), Formatting.Indented));
    }
}