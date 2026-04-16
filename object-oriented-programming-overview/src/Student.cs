using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace object_oriented_programming_overview.src
{
    /// <summary>
    /// Encapsulation
    /// </summary>
    public class Student : Person
    {
        public Guid StudentId { get; set; }
        protected string Major { get; set; }
        private string Address;

        public Student()
        {
            Console.WriteLine("I am a student");
        }

        public Student(Guid studentId, string name, string major)
        {
            StudentId = studentId;
            Name = name;
            Major = major;

            Console.WriteLine("I am a student");
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public void Display()
        {
            Console.WriteLine(JsonConvert.SerializeObject(this));
        }
    }
}
