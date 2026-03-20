namespace Garage.Models
{
    public class Employee
    {
        public int EmployeeId {  get; set; }
        public string EmployeeName { get; set; }
        public byte RoleId { get; set; }
        public byte DepartmentId { get; set; }
        public ushort ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }
    }
}
