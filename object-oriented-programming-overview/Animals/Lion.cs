using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming_overview.Animals
{
    public class Lion : Animal
    {
        public Lion() : base()
        {
            Console.WriteLine("Also, I am a Lion!");
        }

        public override void WhoAmI()
        {
            Console.WriteLine("I am a Lion");
        }
    }
}
