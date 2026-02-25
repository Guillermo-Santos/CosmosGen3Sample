using Cosmos.Kernel.Core.Memory.GarbageCollector;

namespace ThreadingSample;

[BackgroundTask]
public static partial class GCWorker
{
    public static void DoWork()
    {
        while (true)
        {
            Thread.Sleep(5000);
            GarbageCollector.Collect();
        }
    }
}