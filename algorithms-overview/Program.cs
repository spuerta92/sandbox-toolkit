using Newtonsoft.Json;

static class Program
{
    static List<int> HeapSort(List<int> list)
    {
        // time complexity

        // algorithm

        return list;
    }

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

        // quick sort

        // linear search

        // binary search

        // breadth-first search

        // depth-first search

        // hashing

        // recursion

        // trees

        // graphs
    }
}