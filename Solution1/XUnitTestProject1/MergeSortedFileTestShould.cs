using ConsoleApp1;
using ConsoleApp1.MyExts;
using ConsoleApp1.Utils;
using System;
using System.IO;
using System.Text;
using Xunit;
using static ConsoleApp1.Utils.Util;

namespace XUnitTestProject1
{
	public class MergeSortedFileTestShould
	{
		[Fact]
		public void FailWhenInputFilesAreMissing()
		{
			// Arrange a file with just dates
			var file1 = @"InputFile_1.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
			var inputFile1 = Path.Combine(binDirectory, file1);

			if (File.Exists(inputFile1))
				File.Delete(inputFile1);

			Assert.Throws<FileNotFoundException>(() => ApplicationUtils.checkIfFileExists(inputFile1));

			var file2 = @"InputFile_2.data";
			var inputFile2 = Path.Combine(binDirectory, file2);

			if (File.Exists(inputFile2))
				File.Delete(inputFile2);

			Assert.Throws<FileNotFoundException>(() => ApplicationUtils.checkIfFileExists(inputFile2));

		}

		[Fact]
		public void FailWhenInputFilesAreDifferent()
		{
			var file1 = @"InputFile_1.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var inputFile1 = Path.Combine(binDirectory, file1);

			var file2 = @"InputFile_2.data";
			var inputFile2 = Path.Combine(binDirectory, file2);

			var file3 = @"Output.data";
			var inputFile3 = Path.Combine(binDirectory, file3);


			var random = new Random();

			using (var file = new StreamWriter(inputFile1))
			{
				var stringData = random.RandomString((int)random.NextDouble(5.0, 20.0));
				file.WriteLine(stringData);
			}

			using (var file = new StreamWriter(inputFile2))
			{
				var dateTimeData = random.GetRandomDateTime(DateTime.Parse("Jan 1 2019"), DateTime.Parse("Jan 1 2019"));
				file.WriteLine(dateTimeData);
			}

			var fileProcessor = new FileMergeProcessor(inputFile1, inputFile2, inputFile3);

			Assert.Throws<ArgumentException>(() => fileProcessor.Process());

		}

		[Fact]
		public void WhenInputFilesAreDoubles()
		{
			var file1 = @"InputFile_1.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var inputFile1 = Path.Combine(binDirectory, file1);

			var file2 = @"InputFile_2.data";
			var inputFile2 = Path.Combine(binDirectory, file2);

			var file3 = @"Output.data";
			var outputFile = Path.Combine(binDirectory, file3);


			using (var file = new StreamWriter(inputFile1))
			{
				file.WriteLine("1.0");
				file.WriteLine("3.0");
				file.WriteLine("7.0");
				file.WriteLine("9.0");
				file.WriteLine("14.0");
				file.WriteLine("15.0");

			}

			using (var file = new StreamWriter(inputFile2))
			{
				file.WriteLine("2.0");
				file.WriteLine("4.0");
				file.WriteLine("5.0");
				file.WriteLine("6.0");
				file.WriteLine("8.0");
				file.WriteLine("10.0");
				file.WriteLine("11.0");
				file.WriteLine("12.0");
				file.WriteLine("13.0");
			}

			var fileProcessor = new FileMergeProcessor(inputFile1, inputFile2, outputFile);
			fileProcessor.Process();

			using (var streamReader = new StreamReader(outputFile))
			{
				var iterator = streamReader.ToIterator();
				var i = 1;
				while (iterator.MoveNext())
				{					
					Assert.True(i++ == int.Parse(iterator.Current));
				}
			}

		}


		[Fact]
		public void WhenInputFilesAreStrings()
		{
			var file1 = @"InputFile_1.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var inputFile1 = Path.Combine(binDirectory, file1);

			var file2 = @"InputFile_2.data";
			var inputFile2 = Path.Combine(binDirectory, file2);

			var file3 = @"Output.data";
			var outputFile = Path.Combine(binDirectory, file3);

			using (var file = new StreamWriter(inputFile1))
			{
				file.WriteLine("A");
				file.WriteLine("C");
				file.WriteLine("E");
				file.WriteLine("G");
				file.WriteLine("I");
				file.WriteLine("K");

			}

			using (var file = new StreamWriter(inputFile2))
			{
				file.WriteLine("B");
				file.WriteLine("D");
				file.WriteLine("F");
				file.WriteLine("H");
				file.WriteLine("J");
				file.WriteLine("L");
				file.WriteLine("M");
				file.WriteLine("N");
				
			}

			var fileProcessor = new FileMergeProcessor(inputFile1, inputFile2, outputFile);
			fileProcessor.Process();

			using (var streamReader = new StreamReader(outputFile))
			{
				var iterator = streamReader.ToIterator();
				byte i = 65;
				while (iterator.MoveNext())
				{
					Assert.True(Encoding.ASCII.GetString(new byte[] { i++ }) == iterator.Current);
				}
			}

		}


		[Fact]
		public void WhenInputFilesAreDateTime()
		{
			var file1 = @"InputFile_1.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var inputFile1 = Path.Combine(binDirectory, file1);

			var file2 = @"InputFile_2.data";
			var inputFile2 = Path.Combine(binDirectory, file2);

			var file3 = @"Output.data";
			var outputFile = Path.Combine(binDirectory, file3);



			using (var file = new StreamWriter(inputFile1))
			{
				file.WriteLine(new DateTime(2018, 1, 1));
				file.WriteLine(new DateTime(2018, 3, 1));
				file.WriteLine(new DateTime(2018, 5, 1));
				file.WriteLine(new DateTime(2018, 7, 1));
				file.WriteLine(new DateTime(2018, 9, 1));
			}

			using (var file = new StreamWriter(inputFile2))
			{
				file.WriteLine(new DateTime(2018, 2, 1));
				file.WriteLine(new DateTime(2018, 4, 1));
				file.WriteLine(new DateTime(2018, 6, 1));
				file.WriteLine(new DateTime(2018, 8, 1));
				file.WriteLine(new DateTime(2018, 10, 1));
				file.WriteLine(new DateTime(2018, 11, 1));
				file.WriteLine(new DateTime(2018, 12, 1));
			}

			var fileProcessor = new FileMergeProcessor(inputFile1, inputFile2, outputFile);
			fileProcessor.Process();

			using (var streamReader = new StreamReader(outputFile))
			{
				var iterator = streamReader.ToIterator();
				byte i = 1;
				while (iterator.MoveNext())
				{
					Assert.True((new DateTime(2018, i++, 1) - DateTime.Parse(iterator.Current)).Ticks == 0);			
				}
			}

		}

	}

}
