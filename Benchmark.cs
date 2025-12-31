using DataBenchmark.Unique;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataBenchmark
{
    internal class Benchmark
    {
        private List<BenchmarkResult> results = new List<BenchmarkResult>();

        private string CalculatePerformance(double baselineTime, double comparedTime)
        {
            if (comparedTime > baselineTime)
            {
                double factor = comparedTime / baselineTime;
                return $"{factor:F2}x MAIS LENTO";
            }
            else
            {
                double factor = baselineTime / comparedTime;
                return $"{factor:F2}x mais rápido";
            }
        }

        public void CompareInsertUnique(int iterations)
        {
            var listUnique = new ListUnique();
            var hashSetUnique = new HashSetUnique();
            var stopwatch = new Stopwatch();

            Console.WriteLine($"=== Inserção Única - {iterations:N0} elementos ===\n");

            // Teste com ListUnique
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                listUnique.InsertUnique(i);
            }
            stopwatch.Stop();
            var listUniqueTimeMs = stopwatch.Elapsed.TotalMilliseconds;
            var listUniqueTimeUs = stopwatch.Elapsed.TotalMicroseconds;

            results.Add(new BenchmarkResult
            {
                Name = "List - Insert Unique",
                ElapsedMilliseconds = listUniqueTimeMs,
                ElapsedMicroseconds = listUniqueTimeUs,
                Complexity = "O(n²)",
                IsBaseline = true
            });

            stopwatch.Reset();

            // Teste com HashSetUnique
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                hashSetUnique.InsertUnique(i);
            }
            stopwatch.Stop();
            var hashSetUniqueTimeMs = stopwatch.Elapsed.TotalMilliseconds;
            var hashSetUniqueTimeUs = stopwatch.Elapsed.TotalMicroseconds;

            results.Add(new BenchmarkResult
            {
                Name = "HashSet - Insert Unique",
                ElapsedMilliseconds = hashSetUniqueTimeMs,
                ElapsedMicroseconds = hashSetUniqueTimeUs,
                Complexity = "O(1)",
                IsBaseline = false,
                BaselineTime = listUniqueTimeUs
            });
        }

        public void PrintResults()
        {
            if (results.Count == 0)
            {
                Console.WriteLine("\nNenhum resultado para exibir.\n");
                return;
            }

            Console.WriteLine("\n" + new string('=', 100));
            Console.WriteLine("BENCHMARK RESULTS");
            Console.WriteLine(new string('=', 100));
            Console.WriteLine($"{"Operation",-35} {"Time (ms)",12} {"Time (µs)",15} {"Complexity",12} {"Performance",20}");
            Console.WriteLine(new string('-', 100));

            foreach (var result in results)
            {
                string performance = result.IsBaseline 
                    ? "Baseline" 
                    : CalculatePerformance(result.BaselineTime, result.ElapsedMicroseconds);

                Console.WriteLine($"{result.Name,-35} {result.ElapsedMilliseconds,12:F4} {result.ElapsedMicroseconds,15:F2} {result.Complexity,12} {performance,20}");
            }

            Console.WriteLine(new string('=', 100));
        }

        public void Clear()
        {
            results.Clear();
        }

        private class BenchmarkResult
        {
            public string Name { get; set; }
            public double ElapsedMilliseconds { get; set; }
            public double ElapsedMicroseconds { get; set; }
            public string Complexity { get; set; }
            public bool IsBaseline { get; set; }
            public double BaselineTime { get; set; }
        }
    }
}