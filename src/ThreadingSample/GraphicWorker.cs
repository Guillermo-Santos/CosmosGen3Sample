using Cosmos.Kernel.Graphics;

namespace ThreadingSample;

[BackgroundTask]
public static partial class GraphicWorker
{
    public static void DoWork()
    {
        if (Canvas.Width == 0 || Canvas.Height == 0)
            return;

        const int squareSize = 80;
        const int margin = 20;

        int x = Canvas.Width >= (uint)(squareSize + margin * 2)
            ? (int)Canvas.Width - squareSize - margin
            : margin;
        int y = Canvas.Height >= (uint)(squareSize + margin * 2)
            ? (int)Canvas.Height - squareSize - margin
            : margin;

        int frame = 0;

        while (true)
        {
            int phase = frame % 60;
            byte r, g, b;

            if (phase < 10) { r = 255; g = (byte)(phase * 25); b = 0; }
            else if (phase < 20) { r = (byte)(255 - (phase - 10) * 25); g = 255; b = 0; }
            else if (phase < 30) { r = 0; g = 255; b = (byte)((phase - 20) * 25); }
            else if (phase < 40) { r = 0; g = (byte)(255 - (phase - 30) * 25); b = 255; }
            else if (phase < 50) { r = (byte)((phase - 40) * 25); g = 0; b = 255; }
            else { r = 255; g = 0; b = (byte)(255 - (phase - 50) * 25); }

            for (int dy = 0; dy < squareSize; dy++)
            {
                for (int dx = 0; dx < squareSize; dx++)
                {
                    int cx = dx - squareSize / 2;
                    int cy = dy - squareSize / 2;
                    int dist = (cx * cx + cy * cy) * 255 / (squareSize * squareSize / 2);
                    if (dist > 255) dist = 255;

                    int factor = 255 - dist / 2;
                    byte pr = (byte)((r * factor) / 255);
                    byte pg = (byte)((g * factor) / 255);
                    byte pb = (byte)((b * factor) / 255);
                    uint pixelColor = (uint)((pr << 16) | (pg << 8) | pb);

                    Canvas.DrawPixel(pixelColor, x + dx, y + dy);
                }
            }

            frame++;
            Thread.Sleep(100);
        }
    }
}
