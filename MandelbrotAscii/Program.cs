using System;
using System.Numerics;

namespace MandelbrotAscii
{
	class MainClass
	{
		// output dimensions
		static int height = 80;
		static int width = 120;

		// from windows with data
		static double xMin = -2.0;
		static double xMax = 1.0;
		static double yMin = -2.0;
		static double yMax = 2.0;

		// maximum iterations
		static int maxIter = 256;

		public static void Main (string[] args )
		{
			for (int y = 0; y < height; y++) {
				Console.WriteLine ("");
				for (int x = 0; x < width; x++) {
					var bmp = new Bitmap { Height = height, Width = width };
					var window = new Window { XMin = xMin, XMax = xMax, YMin = yMin, YMax = yMax };
					var currentComplexNumber = FromXYPair (x, y, bmp, window);
					var n = GetIterations (currentComplexNumber);
					Console.Write (n < 100 ? " " : "*");
				}
			}
			Console.ReadKey ();
		}

		public static int GetIterations(Complex input)
		{
			var iteration = 0;
			var z = input;
			while (z.Magnitude < 2.0 && iteration < maxIter) {
				z = z * z + input;
				iteration += 1;
			}
			return iteration;
		}


		public static Complex FromXYPair(int x, int y, Bitmap bmp, Window window)
		{
			var xRange = window.XMax - window.XMin; 
			var yRange = window.YMax - window.YMin; 
			var percentX = (double)x / bmp.Width; 
			var percentY = (double)y / bmp.Height;
			var re = xRange * percentX + window.XMin;
			var im = yRange * percentY + window.YMin;
			return new Complex (re, im);
		}



	}

	class Bitmap 
	{
		public int Height {get;set;}
		public int Width {get;set;}
	}

	class Window 
	{
		public double XMin {get;set;}
		public double XMax {get;set;}
		public double YMin {get;set;}
		public double YMax {get;set;}
	}
}
