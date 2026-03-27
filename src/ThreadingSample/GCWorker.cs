using Cosmos.Kernel.Core.Memory.GarbageCollector;

namespace ThreadingSample;

[BackgroundTask]
public static partial class GCWorker
{
    public static void DoWork()
    {
        int lastCollectedCount = 0;
        int sleepMs = 5000;
        
        while (true)
        {
            Thread.Sleep(sleepMs);
            var objectsCollected = GarbageCollector.Collect();
            
            // If many objects were collected, GC sooner; if few, GC later
            if (objectsCollected > 1000)
                sleepMs = Math.Max(1000, sleepMs - 500);  // More frequent
            else if (objectsCollected < 100)
                sleepMs = Math.Min(10000, sleepMs + 500); // Less frequent
                
            lastCollectedCount = objectsCollected;
        }
    }
}