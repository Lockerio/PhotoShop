using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Photoshop
{
    public class Filters
    {
        public static Bitmap AdjustContrast(Bitmap image, float contrast)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color originalColor = image.GetPixel(x, y);

                    int newR = (int)((originalColor.R - 127) * contrast + 127);
                    int newG = (int)((originalColor.G - 127) * contrast + 127);
                    int newB = (int)((originalColor.B - 127) * contrast + 127);

                    newR = Math.Max(0, Math.Min(255, newR));
                    newG = Math.Max(0, Math.Min(255, newG));
                    newB = Math.Max(0, Math.Min(255, newB));

                    Color adjustedColor = Color.FromArgb(newR, newG, newB);
                    adjustedImage.SetPixel(x, y, adjustedColor);
                }
            }

            return adjustedImage;
        }

        public static Bitmap AdjustBrightness(Bitmap image, int brightness)
        {
            Bitmap adjustedImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color originalColor = image.GetPixel(x, y);

                    int newR = Math.Max(0, Math.Min(255, originalColor.R + brightness));
                    int newG = Math.Max(0, Math.Min(255, originalColor.G + brightness));
                    int newB = Math.Max(0, Math.Min(255, originalColor.B + brightness));

                    Color adjustedColor = Color.FromArgb(newR, newG, newB);
                    adjustedImage.SetPixel(x, y, adjustedColor);
                }
            }

            return adjustedImage;
        }

        public static Bitmap ApplyBlur(Bitmap image, int radius)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap adjustedImage = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int totalR = 0, totalG = 0, totalB = 0, count = 0;

                    for (int i = -radius; i <= radius; i++)
                    {
                        for (int j = -radius; j <= radius; j++)
                        {
                            int neighborX = x + i;
                            int neighborY = y + j;

                            if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                            {
                                Color neighborColor = image.GetPixel(neighborX, neighborY);
                                totalR += neighborColor.R;
                                totalG += neighborColor.G;
                                totalB += neighborColor.B;
                                count++;
                            }
                        }
                    }

                    byte avgR = (byte)(totalR / count);
                    byte avgG = (byte)(totalG / count);
                    byte avgB = (byte)(totalB / count);

                    Color blurredColor = Color.FromArgb(avgR, avgG, avgB);
                    adjustedImage.SetPixel(x, y, blurredColor);
                }
            }

            return adjustedImage;
        }
    }
}
