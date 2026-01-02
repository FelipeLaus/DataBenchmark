using System;
using System.IO;
using System.Text.Json;

namespace DataBenchmark
{
    public class MemoryMeasurer
    {
        public static long MeasureSize(Action populateAction)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long before = GC.GetTotalMemory(true);

            populateAction();

            long after = GC.GetTotalMemory(false);

            return after - before;
        }
    }

}
