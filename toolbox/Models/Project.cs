namespace toolbox.Models
{
    public class Project
    {
        public ushort ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal BudgetAmount { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime SysStartTime { get; set; }
    }
}
