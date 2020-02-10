using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.MyExts
{

	public static class RandomExts
	{

		public static double NextDouble(this Random rnd, double min, double max)
		{
			var result = rnd.NextDouble() * (max - min) + min;
			return Math.Round(result, 2);
			//return result;
		}



		public static string RandomString(this Random rnd, int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[rnd.Next(s.Length)]).ToArray());
		}


		public static DateTime GetRandomDateTime(this Random rnd, DateTime? min = null, DateTime? max = null)
		{
			min = min ?? new DateTime(1753, 01, 01);
			max = max ?? new DateTime(9999, 12, 31);

			var range = max.Value - min.Value;
			var randomUpperBound = (Int32)range.TotalSeconds;
			if (randomUpperBound <= 0)
				randomUpperBound = rnd.Next(1, Int32.MaxValue);

			var randTimeSpan = TimeSpan.FromSeconds((Int64)(range.TotalSeconds - rnd.Next(0, randomUpperBound)));
			return min.Value.Add(randTimeSpan);
		}


	}
}
