using DataBenchmark.Unique;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Bogus;

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

        public void CompareSearchDataStructures(int totalUsers)
        {
            var stopwatch = new Stopwatch();

            List<User> users = new List<User>();
            Dictionary<string, User> usersByName = new Dictionary<string, User>();
            SortedDictionary<string, User> usersTree = new SortedDictionary<string, User>();

            var faker = new Faker<User>("pt_BR")
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(u => u.Name, f => f.Name.FullName());

            Console.WriteLine($"=== Busca em Estruturas de Dados - {totalUsers:N0} elementos ===\n");

            // Lista
            long listMemory = MemoryMeasurer.MeasureSize(() =>
            {
                users = new List<User>();
                users.AddRange(faker.Generate(totalUsers / 2));
                users.Add(new User { Id = totalUsers / 2 + 1, Name = "Nome Test" });
                users.AddRange(faker.Generate(totalUsers - (totalUsers / 2 + 1)));
            });

            stopwatch.Start();
            var userFind = users.FirstOrDefault(u => u.Name == "Nome Test");
            stopwatch.Stop();
            var listTimeMs = stopwatch.Elapsed.TotalMilliseconds;
            var listTimeUs = stopwatch.Elapsed.TotalMicroseconds;
            stopwatch.Reset();

            results.Add(new BenchmarkResult
            {
                Name = "List - Search",
                ElapsedMilliseconds = listTimeMs,
                ElapsedMicroseconds = listTimeUs,
                Complexity = "O(n)",
                MemoryBytes = listMemory,
                IsBaseline = true
            });

            // Dictionary
            long dictMemory = MemoryMeasurer.MeasureSize(() =>
            {
                usersByName = new Dictionary<string, User>();
                foreach (var user in faker.Generate(totalUsers / 2))
                {
                    usersByName[user.Name] = user;
                }
                var testUser = new User { Id = totalUsers / 2 + 1, Name = "Nome Test" };
                usersByName[testUser.Name] = testUser;
                foreach (var user in faker.Generate(totalUsers - (totalUsers / 2 + 1)))
                {
                    usersByName[user.Name] = user;
                }
            });

            stopwatch.Start();
            usersByName.TryGetValue("Nome Test", out var foundUser);
            stopwatch.Stop();
            var dictTimeMs = stopwatch.Elapsed.TotalMilliseconds;
            var dictTimeUs = stopwatch.Elapsed.TotalMicroseconds;
            stopwatch.Reset();

            results.Add(new BenchmarkResult
            {
                Name = "Dictionary - Search",
                ElapsedMilliseconds = dictTimeMs,
                ElapsedMicroseconds = dictTimeUs,
                Complexity = "O(1)",
                MemoryBytes = dictMemory,
                IsBaseline = false,
                BaselineTime = listTimeUs
            });

            // SortedDictionary (Árvore Red-Black)
            long treeMemory = MemoryMeasurer.MeasureSize(() =>
            {
                usersTree = new SortedDictionary<string, User>();
                foreach (var user in faker.Generate(totalUsers / 2))
                {
                    usersTree[user.Name] = user;
                }
                var testUser = new User { Id = totalUsers / 2 + 1, Name = "Nome Test" };
                usersTree[testUser.Name] = testUser;
                foreach (var user in faker.Generate(totalUsers - (totalUsers / 2 + 1)))
                {
                    usersTree[user.Name] = user;
                }
            });

            stopwatch.Start();
            usersTree.TryGetValue("Nome Test", out var foundUserTree);
            stopwatch.Stop();
            var treeTimeMs = stopwatch.Elapsed.TotalMilliseconds;
            var treeTimeUs = stopwatch.Elapsed.TotalMicroseconds;

            results.Add(new BenchmarkResult
            {
                Name = "SortedDictionary - Search",
                ElapsedMilliseconds = treeTimeMs,
                ElapsedMicroseconds = treeTimeUs,
                Complexity = "O(log n)",
                MemoryBytes = treeMemory,
                IsBaseline = false,
                BaselineTime = listTimeUs
            });
        }

        public void PrintResults()
        {
            if (results.Count == 0)
            {
                Console.WriteLine("\nNenhum resultado para exibir.\n");
                return;
            }

            Console.WriteLine("\n" + new string('=', 120));
            Console.WriteLine("BENCHMARK RESULTS");
            Console.WriteLine(new string('=', 120));
            Console.WriteLine($"{"Operation",-35} {"Time (ms)",12} {"Time (µs)",15} {"Complexity",12} {"Memory (bytes)",18} {"Performance",20}");
            Console.WriteLine(new string('-', 120));

            foreach (var result in results)
            {
                string performance = result.IsBaseline 
                    ? "Baseline" 
                    : CalculatePerformance(result.BaselineTime, result.ElapsedMicroseconds);

                string memory = result.MemoryBytes > 0 ? $"{result.MemoryBytes:N0}" : "-";

                Console.WriteLine($"{result.Name,-35} {result.ElapsedMilliseconds,12:F4} {result.ElapsedMicroseconds,15:F2} {result.Complexity,12} {memory,18} {performance,20}");
            }

            Console.WriteLine(new string('=', 120));
        }

        public void Clear()
        {
            results.Clear();
        }
    }
}