using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1.MyExts
{
	public static class StreamExts
	{
		public static IEnumerator<string> ToIterator(this StreamReader reader)
		{
			string line;
			while ((line = reader.ReadLine()) != null)
			{
				yield return line;
			}
		}
	}
}
