using ConsoleApp1.Utils;
using System;
using System.IO;
using static System.Console;

namespace ConsoleApp1
{

	/*
	* You are given a file formatted like this:
	* CUSIP
	* Price
	* Price
	* Price...
	* 
	*    As stated in the problem statement the solution do
	*    
	*  Think of it as a file of price ticks for a set of bonds identified by their CUSIPs.
	*	You can assume a CUSIP is just an 8-character alphanumeric string. Each CUSIP may have any number of prices (e.g., 95.752, 101.255) following it in sequence, one per line.
	*	The prices can be considered to be ordered by time in ascending order, earliest to latest. 
	*	Write a C# program that will print the opening, lowest, highest, closing price for each CUSIP in the file.
	*	DO NOT assume the entire file can fit in memory.
	*/

	/// <summary>
	/// The program exepcts:
	/// INPUT FILE @bondprices.data file to be present in the bin folder
	/// NO OUTPUT FILE IS PRODUCED
	/// The program prints every CUSIP along with opening, lowest, highest, closing price on the console
	/// </summary>
	class Program
	{
		private static readonly string PricingFile = @"bondprices.data";
		static void Main(string[] args)
		{
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
			var fqInputFile = Path.Combine(binDirectory, PricingFile);
			
			// check if file exists
			ApplicationUtils.checkIfBondFileExists(fqInputFile);

			// process the file
			var fileProcessor = new BondFileProcessor(fqInputFile);
			fileProcessor.Process();


			WriteLine("Press any key to quit.");
			ReadLine();
		}

		
	}

	
}
