using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using toolbox.DummyRepositories;
using toolbox.Models;

namespace core_rest_api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeMockRepository _repository;
        private readonly ILogger<EmployeesController> _logger;
        private readonly EventLog _eventLog;

        public EmployeesController(IEmployeeMockRepository repository, ILogger<EmployeesController> logger)
        {
            _repository = repository;
            _logger = logger;
            _eventLog = new EventLog("Application")
            {
                Source = "MyCompanyApp"
            };

        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _eventLog.WriteEntry("Retrieve all employees", EventLogEntryType.Information);  // logging to event viewer
            return await _repository.GetEmployees();
        }

        [HttpGet("[action]/{employeeId}")]
        public async Task<Employee> GetEmployee(int employeeId)
        {
            _eventLog.WriteEntry($"Employee with Id: {employeeId} was retrieved", EventLogEntryType.Information);  // logging to event viewer
            return await _repository.GetEmployeeById(employeeId);
        }
    }
}
