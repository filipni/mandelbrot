using System.Drawing;
using System.Numerics;

namespace Mandelbrot_Set_Generator
{
    class Program
    {
        private const double XStart = -2.2;
        private const double XStop = 0.8;
        private const double XRange = XStop - XStart;

        private const double YStart = -1.3;
        private const double YStop = 1.3;
        private const double YRange = YStop - YStart;

        private const int ResolutionBase = 1000;
        private readonly int IMAGE_WIDTH;
        private readonly int IMAGE_HEIGHT;

        private const int MAX_ITERATIONS = 1000;

        private const string SAVE_PATH = @"Mandelbrot.PNG";

        private readonly Color[] ColorMap = new Color[] {
            Color.FromArgb(66, 30, 15),
            Color.FromArgb(27, 7, 26),
            Color.FromArgb(9, 1, 47),
            Color.FromArgb(4, 4, 73),
            Color.FromArgb(0, 7, 100),
            Color.FromArgb(12, 44, 138),
            Color.FromArgb(24, 82, 177),
            Color.FromArgb(57, 125, 209),
            Color.FromArgb(134, 181, 229),
            Color.FromArgb(211, 236, 248),
            Color.FromArgb(241, 233, 191),
            Color.FromArgb(248, 201, 95),
            Color.FromArgb(255, 170, 0),
            Color.FromArgb(204, 128, 0),
            Color.FromArgb(153, 87, 0),
            Color.FromArgb(106, 52, 3)
        };

        private Program()
        {
            // Scale sides to correct aspect ratio
            if (XRange >= YRange)
            {
                IMAGE_WIDTH = ResolutionBase;
                IMAGE_HEIGHT = (int) (ResolutionBase * YRange / XRange);
            }
            else
            {
                IMAGE_HEIGHT = ResolutionBase;
                IMAGE_WIDTH = (int) (ResolutionBase * XRange / YRange);
            }

            GenerateSetImage();
        }

        private void GenerateSetImage()
        {
            Bitmap Image = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT);

            for (int x = 0; x < IMAGE_WIDTH; x++)
            {
                for (int y = 0; y < IMAGE_HEIGHT; y++)
                {
                    double a = (x + 1.0) / IMAGE_WIDTH * XRange + XStart;
                    double b = (y + 1.0) / IMAGE_HEIGHT * YRange + YStart;
                    Complex c = new Complex(a, b);
                    Complex z = new Complex();
                    int iterations = 0;

                    while (z.Magnitude < 2 && iterations < MAX_ITERATIONS)
                    {
                        z = z * z + c;
                        iterations++;
                    }

                    if (iterations < MAX_ITERATIONS)
                        Image.SetPixel(x, y, ColorMap[iterations % ColorMap.Length]);
                    else
                        Image.SetPixel(x, y, Color.Black);
                }
            }

            Image.Save(SAVE_PATH);
        }

        static void Main(string[] args)
        {
            new Program();
        }
    }
}
