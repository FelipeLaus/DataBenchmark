using System;
using System.Collections.Generic;
using System.Text;

namespace DataBenchmark
{
    public class BenchmarkResult
    {
        public string Name { get; set; }
        public double ElapsedMilliseconds { get; set; }
        public double ElapsedMicroseconds { get; set; }
        public string Complexity { get; set; }
        public long MemoryBytes { get; set; }
        public bool IsBaseline { get; set; }
        public double BaselineTime { get; set; }
    }
}
