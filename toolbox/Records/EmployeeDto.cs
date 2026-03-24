namespace toolbox.Records
{
    public record EmployeeDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public byte RoleId { get; set; }
        public byte DepartmentId { get; set; }
        public short ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? TerminationDate { get; set; }
    }
}
