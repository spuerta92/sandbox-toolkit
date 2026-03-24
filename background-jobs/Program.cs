using Hangfire;

class Program
{
    static void Main()
    {
        GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseColouredConsoleLogProvider()
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage("Server=(localdb)\\MSSQLLocalDB; Database=MyCompany; Integrated Security=true; Trusted_Connection=true;");

        BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));

        using (var server = new BackgroundJobServer())
        {
            Console.ReadLine();
        }
    }
}