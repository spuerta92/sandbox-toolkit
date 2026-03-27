using toolbox.Models;

namespace toolbox.DummyRepositories
{
    public interface IEmployeeMockRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployeeById(int employeeId);
    }
}
