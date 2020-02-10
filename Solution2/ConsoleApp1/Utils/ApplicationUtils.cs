using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1.Utils
{
	public static class ApplicationUtils
	{
		public static void checkIfBondFileExists(string inputFile)
		{
			if (!File.Exists(inputFile))
			{
				throw new FileNotFoundException($"ERROR: file {inputFile} does not exist.");
			}
		}

	}
}
