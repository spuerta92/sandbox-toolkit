using entity_framework;
using entity_framework.repository;
using entity_framework.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

static class Program
{
    /// <summary>
    /// Entity framework overview
    /// </summary>
    static void Main(string[] args)
    {
        // NON-DI Pattern
        //var options = new DbContextOptionsBuilder<MyCompanyContext>()
        //    .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyCompany;Trusted_Connection=True;TrustServerCertificate=True;")
        //    .Options;

        //using var context = new MyCompanyContext(options);
        //var repository = new EmployeeRepository(context);

        //var employees = repository.GetEmployees();
        //Console.Write(JsonConvert.SerializeObject(employees, Formatting.Indented));

        // DI
        string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        string path = Path.Combine(root, "appsettings.json");

        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(path, optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<MyCompanyContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyCompanyDb")));

                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            })
            .Build();

        using IServiceScope scope = host.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        var repository = services.GetRequiredService<IEmployeeRepository>();
        var employees = repository.GetEmployees();
        Console.Write(JsonConvert.SerializeObject(employees, Formatting.Indented));
    }
}