using toolbox.Enums;
using toolbox.Models;

namespace toolbox.DummyRepositories
{
    public class EmployeeMockRepository : IEmployeeMockRepository
    {
        private IEnumerable<Employee> _employees = new List<Employee>()
        {
            new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Charlie",
                DepartmentId = (byte)Departments.InvestmentResearch,
                ProjectId = (short)Projects.BudgetPlanningPlatformUpgrade,
                RoleId = (byte)Roles.PortfolioManager,
                StartDate = DateTime.Now.AddYears(-2)
            },
            new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Will",
                DepartmentId = (byte)Departments.CorporateFinance,
                ProjectId = (short)Projects.ESGRiskIntegration,
                RoleId = (byte)Roles.Accountant,
                StartDate = DateTime.Now.AddMonths(1).AddYears(-4)
            },
            new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Arthur",
                DepartmentId = (byte)Departments.Treasury,
                ProjectId = (short)Projects.ExpenseOptimizationInitiative,
                RoleId = (byte)Roles.TreasuryAnalyst,
                StartDate = DateTime.Now.AddMonths(-5).AddYears(-3)
            },
            new Employee
            {
                EmployeeId = 4,
                EmployeeName = "Jess",
                DepartmentId = (byte)Departments.CorporateFinance,
                ProjectId = (short)Projects.ClientProfitabilityAnalytics,
                RoleId = (byte)Roles.FinanceDirector,
                StartDate = DateTime.Now.AddMonths(-3).AddYears(-2)
            },
            new Employee
            {
                EmployeeId = 5,
                EmployeeName = "Candace",
                DepartmentId = (byte)Departments.Operations,
                ProjectId = (short)Projects.DataWarehouseFinanceMart,
                RoleId = (byte)Roles.OperationsAnalyst,
                StartDate = DateTime.Now.AddYears(-4)
            },
            new Employee
            {
                EmployeeId = 6,
                EmployeeName = "Julia",
                DepartmentId = (byte)Departments.RiskAndCompliance,
                ProjectId = (short)Projects.PrivateMarketsReportingEnhancement,
                RoleId = (byte)Roles.ComplianceOfficer,
                StartDate = DateTime.Now.AddYears(-3)
            }
        };

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return _employees;
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return _employees.First(e => e.EmployeeId.Equals(employeeId));
        }
    }
}
