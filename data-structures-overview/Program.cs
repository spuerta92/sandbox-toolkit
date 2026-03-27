using Newtonsoft.Json;
using System.Collections;
using System.Collections.ObjectModel;
using toolbox.Enums;
using toolbox.Models;

static class Program
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T value)
        {
            Value = value;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(TreeNode<T> child)
        {
            Children.Add(child);
        }
    }

    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T>? Left { get; set; }
        public BinaryTreeNode<T>? Right { get; set; }

        public BinaryTreeNode(T value)
        {
            Value = value;
        }
    }

    public class Graph<T>
    {
        private readonly Dictionary<T, List<T>> _adjacencyList = new();
        //private readonly Dictionary<T, HashSet<T>> _adjacencyList = new();    // can use hashset to avoid duplicate edges

        public void AddVertex(T vertex)
        {
            if (!_adjacencyList.ContainsKey(vertex))
            {
                _adjacencyList[vertex] = new List<T>();
            }
        }

        public void AddEdge(T from, T to)
        {
            AddVertex(from);
            AddVertex(to);

            _adjacencyList[from].Add(to);
        }

        public IEnumerable<T> GetNeighbors(T vertex)
        {
            return _adjacencyList.TryGetValue(vertex, out var neighbors)
                ? neighbors
                : Array.Empty<T>();
        }

        public void AddUndirectedEdge(T a, T b)
        {
            AddVertex(a);
            AddVertex(b);

            _adjacencyList[a].Add(b);
            _adjacencyList[b].Add(a);
        }

        public void DisplayGraph()
        {
            foreach (var kvp in _adjacencyList)
            {
                Console.Write($"{kvp.Key}: ");
                Console.WriteLine(string.Join(", ", kvp.Value));
            }
        }
    }

    static void DisplayTree<T>(TreeNode<T> node, int level)
    {
        Console.WriteLine(new string('-', level * 2) + node.Value);

        foreach (var child in node.Children)
        {
            DisplayTree(child, level + 1);
        }
    }

    /// <summary>
    /// Overview of core data structures (array, list, collection, hashtable, hashset, dictionary, key/value pair, tuple, stack, queue, linked list, trees, graphs)
    /// </summary>
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

        // hash set (unordered list (essentially a dictionary but with a dummy value for the key - Stores a set of unique values/items.))
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

        // tree
        var root = new TreeNode<string>("MyCompany");

        var treasury = new TreeNode<string>(Departments.Treasury.ToString());
        var riskCompliance = new TreeNode<string>(Departments.RiskAndCompliance.ToString());
        var operations = new TreeNode<string>(Departments.Operations.ToString());
        var investmentResearch = new TreeNode<string>(Departments.InvestmentResearch.ToString());

        var charlie = new TreeNode<string>("Charlie");
        var will = new TreeNode<string>("Will");
        var jess = new TreeNode<string>("Jess");
        var arthur = new TreeNode<string>("Arthur");

        root.AddChild(treasury);
        root.AddChild(riskCompliance);
        root.AddChild(operations);
        root.AddChild(investmentResearch);

        treasury.AddChild(charlie);
        riskCompliance.AddChild(will);
        operations.AddChild(jess);
        investmentResearch.AddChild(arthur);

        DisplayTree(root, 0);
        Console.WriteLine();

        // graph
        var graph = new Graph<string>();

        graph.AddEdge("A", "B");
        graph.AddEdge("A", "C");
        graph.AddEdge("B", "D");
        graph.AddEdge("C", "D");

        graph.DisplayGraph();
        Console.WriteLine();
    }
}
