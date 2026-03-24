namespace toolbox.Records
{
    public record ProjectDto
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public decimal BudgetAmount { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime SysStartTime { get; set; }
    }
}
