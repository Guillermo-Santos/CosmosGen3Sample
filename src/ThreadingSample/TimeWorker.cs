using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Cosmos.Kernel.System.Graphics;
using Cosmos.Kernel.System.Graphics.Fonts;

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

        var canvas = FullScreenCanvas.GetFullScreenCanvas();

        var sb = new StringBuilder();

        var font = PCScreenFont.DefaultFont;
        var charwidth = font.Width;
        var x = (int)(canvas.Mode.Width - (22 * charwidth)) - 2;
        var y = 8;
        
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


            canvas.DrawFilledRectangle(Color.Black, x, y, (22 * charwidth) + 4, font.Height + 4, preventOffBoundPixels: true);
            canvas.DrawString(timeString, font, Color.Gray, x, y);
            canvas.Display();

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
