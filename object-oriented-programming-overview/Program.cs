using object_oriented_programming_overview;
using object_oriented_programming_overview.Animals;
using object_oriented_programming_overview.Fruits;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Abstraction/Polymorphism using an abstract class");
        var lion = new Lion();
        Animal lion2 = new Lion();

        Console.WriteLine("Abstraction/Polymorphism using an interface");
        var apple = new Apple();
        IFruit apple2 = new Apple();

        Console.WriteLine("Encapsulation, Inheritance");
        var student = new Student(); 
    }
}