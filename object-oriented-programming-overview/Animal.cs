using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming_overview
{
    /// <summary>
    /// "IS-A" relationship - blue print
    /// canot be used to create objects
    /// </summary>
    public abstract class Animal
    {
        public Animal()
        {
            Console.WriteLine("I am an Animal");
        }

        public int Id { get; set; } = -1;
        public string Name { get; set; } = "Unknown";

        // must override
        public abstract void WhoAmI();

        // can override
        public virtual void WhereDoILive()
        {
            Console.WriteLine("I don't know");
        }

        public virtual void WhatDoIEat()
        {
            Console.WriteLine("I don't know");
        }


        public void Display()
        {
            Console.WriteLine($"Id: {Id} Name: {Name}");
        }
    }
}
