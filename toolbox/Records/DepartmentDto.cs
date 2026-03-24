namespace toolbox.Records
{
    public record DepartmentDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public DateTime SysStartTime { get; set; }
    }
}
