using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using toolbox.Enums;
using toolbox.Models;

static class Program
{
    static void Main()
    {
        // array
        var employeeArray = new Employee[]
        {
            new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Charlie"
            },
            new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Will"
            },
            new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Arthur"
            }
        };
        Console.WriteLine("Array");
        Console.WriteLine(JsonConvert.SerializeObject(employeeArray));
        Console.WriteLine();

        // list 
        var employeeList = new List<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Charlie"
            },
            new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Will"
            },
            new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Arthur"
            }
        };
        Console.WriteLine("List");
        Console.WriteLine(JsonConvert.SerializeObject(employeeList));
        Console.WriteLine();

        // collection
        var employeeCollection = new Collection<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Charlie"
            },
            new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Will"
            },
            new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Arthur"
            }
        };
        Console.WriteLine("Collections");
        Console.WriteLine(JsonConvert.SerializeObject(employeeCollection));
        Console.WriteLine();

        // (enumerable -> collection -> list)
        IEnumerable<Employee> employeeEnumerable = employeeList;
        IEnumerable<Employee> EmployeeEnumberable2 = employeeCollection;
        Console.WriteLine("IEnumerable (List)");
        Console.WriteLine(JsonConvert.SerializeObject(employeeEnumerable));
        Console.WriteLine();
        Console.WriteLine("IEnumerable (Collection)");
        Console.WriteLine(JsonConvert.SerializeObject(EmployeeEnumberable2));
        Console.WriteLine();

        // hash table (non-generic)
        var employeeHashTable = new Hashtable();
        employeeHashTable["name"] = "Charlie";
        employeeHashTable["id"] = 1;

        // hash set
        var employeeHashSet = new HashSet<Employee>()
        {
            new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Charlie"
            },
            new Employee
            {
                EmployeeId = 2,
                EmployeeName = "Will"
            },
            new Employee
            {
                EmployeeId = 3,
                EmployeeName = "Arthur"
            }
        };
        Console.WriteLine("Hashset");
        Console.WriteLine(JsonConvert.SerializeObject(employeeHashSet));
        Console.WriteLine();

        // dictionary (generic)
        var employeeDictionary = new Dictionary<Guid, Employee>
        {
            { Guid.NewGuid(), new Employee { EmployeeId = 1, EmployeeName = "Charlie"} },
            { Guid.NewGuid(), new Employee { EmployeeId = 2, EmployeeName = "Will"} },
            { Guid.NewGuid(), new Employee { EmployeeId = 3, EmployeeName = "Arthur"} }
        };
        Console.WriteLine("Dictionary");
        Console.WriteLine(JsonConvert.SerializeObject(employeeDictionary));
        Console.WriteLine();

        // key/value pair
        var employeeKV = new KeyValuePair<Guid, Employee>(Guid.NewGuid(), new Employee { EmployeeId = 1, EmployeeName = "Charlie" });
        Console.WriteLine("Key Value Pair");
        Console.WriteLine(JsonConvert.SerializeObject(employeeKV));
        Console.WriteLine();

        // tuple
        var employeeTuple = new Tuple<Guid, Employee, Departments>(Guid.NewGuid(), new Employee { EmployeeId = 1, EmployeeName = "Charlie" }, Departments.InvestmentResearch);
        Console.WriteLine("Tuple");
        Console.WriteLine(JsonConvert.SerializeObject(employeeTuple));
        Console.WriteLine();

        // stack (LIFO)
        var employeeStack = new Stack<Employee>();
        employeeStack.Push(new Employee { EmployeeId = 1, EmployeeName = "Charlie" });
        employeeStack.Push(new Employee { EmployeeId = 2, EmployeeName = "Will" });
        employeeStack.Push(new Employee { EmployeeId = 3, EmployeeName = "Arthur" });
        Console.WriteLine("Stack");
        Console.WriteLine(JsonConvert.SerializeObject(employeeStack));
        Console.WriteLine();

        // queue (FIFO)
        var employeeQueue = new Queue<Employee>();
        employeeQueue.Enqueue(new Employee { EmployeeId = 1, EmployeeName = "Charlie" });
        employeeQueue.Enqueue(new Employee { EmployeeId = 2, EmployeeName = "Will" });
        employeeQueue.Enqueue(new Employee { EmployeeId = 3, EmployeeName = "Arthur" });
        Console.WriteLine("Queue");
        Console.WriteLine(JsonConvert.SerializeObject(employeeQueue));
        Console.WriteLine();

        // linked list
        var employeeLinkedList = new LinkedList<Employee>();
        employeeLinkedList.AddLast(new Employee { EmployeeId = 1, EmployeeName = "Charlie" });
        employeeLinkedList.AddLast(new Employee { EmployeeId = 2, EmployeeName = "Will" });
        employeeLinkedList.AddLast(new Employee { EmployeeId = 3, EmployeeName = "Arthur" });
        Console.WriteLine("Linked List");
        Console.WriteLine(JsonConvert.SerializeObject(employeeLinkedList));
        Console.WriteLine();
    }
}
