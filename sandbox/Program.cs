using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Program
{
    public static void Main()
    {
        var fruits = new List<Fruit>();
        //fruits = null;
        var ids = fruits.Select(f => f.Id).ToList();
        var fruit = fruits.FirstOrDefault();
        if (fruit == null)
        {
            Console.WriteLine("Fruit is null");
        }
        Console.Write(fruit.Id);

    }
}

public class Fruit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ExpirationDate { get; set; }
}