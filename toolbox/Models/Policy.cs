namespace toolbox.Models
{
    public class Policy
    {
        //public string Version { get; set; }
        public Statement Statement { get; set; }
    }

    public class Statement
    {
        public string Sid { get; set; }
        public string Effect { get; set; }
        public Principal Principal { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public Condition Condition { get; set; }
    }

    public class Principal
    {
        public string Service { get; set; }
        //public string AWS { get; set; }
    }

    public class Condition
    {
        public Dictionary<string, string> ArnEquals { get; set; }
        //public Dictionary<string, string> StringEquals { get; set; }
    }

}
