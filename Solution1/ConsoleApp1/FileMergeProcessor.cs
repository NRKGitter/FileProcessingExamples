using ConsoleApp1.MyExts;
using ConsoleApp1.Utils;
using System.IO;

namespace ConsoleApp1
{
	public class FileMergeProcessor
	{
		private string fullQualifiedDataFile1;
		private string fullQualifiedDataFile2;
		private string fullQualifiedOutputFile;

		public FileMergeProcessor(string fullQualifiedDataFile1, string fullQualifiedDataFile2, string fullQualifiedOutputFile)
		{
			this.fullQualifiedDataFile1 = fullQualifiedDataFile1;
			this.fullQualifiedDataFile2 = fullQualifiedDataFile2;
			this.fullQualifiedOutputFile = fullQualifiedOutputFile;
		}

		public void Process()
		{
			using (var reader1 = new StreamReader(fullQualifiedDataFile1))
			using (var reader2 = new StreamReader(fullQualifiedDataFile2))
			using (var writer = new StreamWriter(fullQualifiedOutputFile))
			{
				foreach (var value in Util.MergeSortedValueObjects(reader1.ToIterator(), reader2.ToIterator()))
					writer.WriteLine(value);
			}
		}
	}
}