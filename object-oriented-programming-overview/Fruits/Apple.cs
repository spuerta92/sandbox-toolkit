using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming_overview.Fruits
{
    public class Apple : IFruit
    {
        public Apple() 
        {
            Console.WriteLine("I am an Apple");
        }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Vendor { get; set; } 


        public void DisplayDescription()
        {
            Console.WriteLine($"Name: {Name}, Color: {Color}, Type: {Type}, Vendor: {Vendor}");
        }
    }
}
