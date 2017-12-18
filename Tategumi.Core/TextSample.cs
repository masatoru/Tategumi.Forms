using System;
using SkiaSharp;

namespace Tategumi.Core
{
    public class TextSample
    {
        public static void DrawSample(SKCanvas canvas, int width, int height)
        {
            canvas.DrawColor(SKColors.White);

            using (var paint = new SKPaint())
            {
                paint.TextSize = 64.0f;
                paint.IsAntialias = true;
                paint.Color = (SKColor)0xFF4281A4;
                paint.IsStroke = false;

                canvas.DrawText("SkiaSharp", width / 2f, height / 2f, paint);
            }
        }
    }
}
