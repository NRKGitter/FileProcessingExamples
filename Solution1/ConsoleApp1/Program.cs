using System;
using System.IO;
using static ConsoleApp1.Utils.Util;

namespace ConsoleApp1
{
	/*
	*	Given two sorted files, write a C# program to merge them while preserving sort order.
	*	The program should determine what data type is used in input files (DateTime, Double, String in that sequence) and merge them accordingly.
	*	DO NOT assume either of these files can fit in memory.
	*
	*/

	/// <summary>
	/// The program hnadles file merging of DateTime, Double and Strings
	/// InputFile_1.Data file to be present in the bin folder
	/// InputFile_2.Data file to be present in the bin folder
	/// The proram produces OUTPUT FILE Output.data in the BIN Folder  
	/// ************************* Some SAMPLE files are provided in the Backup folder under bin
	/// </summary>
	class Program
	{
		private static readonly string DataFile1 = @"InputFile_1.data";
		private static readonly string DataFile2 = @"InputFile_2.data";
		private static readonly string OutputFile = @"Output.data";
		static void Main(string[] args)
		{
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fullQualifiedDataFile1 = Path.Combine(binDirectory, DataFile1);
			var fullQualifiedDataFile2 = Path.Combine(binDirectory, DataFile2);
			var fullQualifiedOutputFile = Path.Combine(binDirectory, OutputFile);

			ApplicationUtils.checkIfFileExists(fullQualifiedDataFile1);
			ApplicationUtils.checkIfFileExists(fullQualifiedDataFile2);

			var fileProcessor = new FileMergeProcessor(fullQualifiedDataFile1, fullQualifiedDataFile2, fullQualifiedOutputFile);

			fileProcessor.Process();

		}
	}
}
