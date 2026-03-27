using Newtonsoft.Json;

static class Program
{
    static List<int> HeapSort(List<int> list)
    {
        // time complexity

        // algorithm

        return list;
    }

    static List<int> MergeSort(List<int> list)
    {
        // time complexity

        // algorithm

        return list;
    }

    static List<int> QuickSort(List<int> list)
    {
        // time complexity

        // algorithm

        return list;
    }

    static Tuple<int, int> LinearSearch(List<int> list, int target)
    {
        // time complexity

        // algorithm
        var index = 0;
        var count = 0;

        return new Tuple<int, int>(index, count);
    }

    static Tuple<int, int> BinarySearch(List<int> list, int target)
    {
        // time complexity

        // algorithm
        var index = 0;
        var count = 0;

        return new Tuple<int, int>(index, count);
    }

    static Tuple<int, int> BreadthFirstSearch(List<int> list, int target)
    {
        // time complexity

        // algorithm
        var index = 0;
        var count = 0;

        return new Tuple<int, int>(index, count);
    }

    static Tuple<int, int> DepthFirstSearch(List<int> list, int target)
    {
        // time complexity

        // algorithm
        var index = 0;
        var count = 0;

        return new Tuple<int, int>(index, count);
    }

    static int Hashing()
    {
        // time complexity

        // algorithm
        var hashing = 0;

        return hashing;
    }

    static int Recursion(int input, int count)
    {
        if (count > 100) return count;
        count = count + 1;
        var result = input + Recursion(input, count);
        for (var i = 0; i < count; i++)
        {
            Console.Write("|");
        }
        Console.Write($"--> {count}");
        Console.WriteLine();
        return result;
    }

    /// <summary>
    /// Overview of core algorithms
    /// </summary>
    static void Main()
    {
        var numbers = new List<int>
        {
            482, 17, 903, 256, 771, 64, 390, 998, 145, 623,
            712, 88, 534, 301, 947, 219, 670, 41, 856, 193,
            764, 509, 12, 681, 432, 975, 278, 156, 820, 367,
            594, 733, 25, 908, 461, 102, 687, 314, 849, 57,
            926, 238, 775, 490, 163, 604, 351, 992, 76, 547,
            280, 819, 134, 665, 403, 951, 210, 742, 519, 30,
            884, 297, 658, 171, 930, 446, 599, 83, 768, 324,
            915, 251, 682, 140, 537, 996, 389, 72, 844, 213,
            701, 478, 19, 862, 335, 620, 157, 973, 286, 541,
            90, 754, 418, 629, 204, 887, 363, 146, 958, 507 
        };

        // heap sort
        var heap_sorted = HeapSort(numbers);
        Console.WriteLine(JsonConvert.SerializeObject(heap_sorted));

        // merge sort
        var merge_sorted = MergeSort(numbers);
        Console.WriteLine(JsonConvert.SerializeObject(heap_sorted));

        // quick sort
        var quick_sorted = QuickSort(numbers);
        Console.WriteLine(JsonConvert.SerializeObject(heap_sorted));

        // linear search
        var linearResult = LinearSearch(numbers, 446);
        Console.WriteLine($"index: {linearResult.Item1}, count: {linearResult.Item2}");

        // binary search
        var binaryResult = BinarySearch(numbers, 446);
        Console.WriteLine($"index: {binaryResult.Item1}, count: {binaryResult.Item2}");

        // breadth-first search
        var bfsResult = BreadthFirstSearch(numbers, 446);
        Console.WriteLine($"index: {bfsResult.Item1}, count: {bfsResult.Item2}");

        // depth-first search
        var dfsResult = DepthFirstSearch(numbers, 446);
        Console.WriteLine($"index: {dfsResult.Item1}, count: {dfsResult.Item2}");

        // hashing (function used in hash tables)

        // recursion
        Recursion(1, 0);
    }
}