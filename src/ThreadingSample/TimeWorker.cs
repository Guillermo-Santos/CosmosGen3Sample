using System.Runtime.CompilerServices;
using System.Text;
using Cosmos.Kernel.Graphics;
using Cosmos.Kernel.Graphics.Fonts;

namespace ThreadingSample;

[BackgroundTask]
public static class TimeWorker
{
    public static void DoWork()
    {
        // Wait until the console is initialized
        while(!KernelConsole.IsInitialized)
        {
            Thread.Sleep(100);
        }

        var sb = new StringBuilder();
        while (true)
        {
            var now = DateTime.Now;
            sb.Append("Current Time: ");
            sb.Append(now.Hour.ToString("D2"));
            sb.Append(':');
            sb.Append(now.Minute.ToString("D2"));
            sb.Append(':');
            sb.Append(now.Second.ToString("D2"));

            string timeString = sb.ToString();
            sb.Clear();
            Canvas.DrawRectangle(Color.Black, (int)(Canvas.Width - (timeString.Length * PCScreenFont.CharWidth)) - 2, 8, (timeString.Length * PCScreenFont.CharWidth) + 4, PCScreenFont.CharHeight + 4);
            Canvas.DrawString(timeString, (int)(Canvas.Width - (timeString.Length * PCScreenFont.CharWidth)) - 2, 8, 0xFFFFFF);

            Thread.Sleep(1000);
        }
    }

    // What the source generator would produce
    [ModuleInitializer]
    public static void Initialize()
    {
        Thread timeThread = new Thread(DoWork);
        timeThread.IsBackground = true;
        timeThread.Start();
    }
}
