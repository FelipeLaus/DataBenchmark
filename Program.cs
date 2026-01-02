namespace DataBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var benchmark = new Benchmark();

            // Benchmark 1: Comparar inserção única
            benchmark.CompareInsertUnique(10000);
            benchmark.PrintResults();
            benchmark.Clear();

            Console.WriteLine("\n");

            // Benchmark 2: Comparar busca em estruturas de dados
            benchmark.CompareSearchDataStructures(300);
            benchmark.PrintResults();

            Console.ReadKey();
        }
    }
}
