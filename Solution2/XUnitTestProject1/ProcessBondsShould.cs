using ConsoleApp1;
using ConsoleApp1.Utils;
using System;
using System.IO;
using Xunit;
using XUnitTestProject1.MyTestExts;

namespace XUnitTestProject1
{


	public class ProcessBondShould
	{

		/// <summary>
		///  The first test was generates specific cusips and corresponding specific prices and compares the 
		///  generated result with the expexted one
		/// </summary>
		[Fact]
		public void ProcessBondsWithPricesFrom0to9()
		{
			// Arrange
			var result = @"Cusip=00000000 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=11111111 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=22222222 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=33333333 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=44444444 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=55555555 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=66666666 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=77777777 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=88888888 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
Cusip=99999999 OpeningPrice=0 LowestPrice=0 HighestPrice=9 ClosingPrice=9
";


			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);

			using (var file = new StreamWriter(fqInputFile))
			{
				for (int i = 0; i < 10; i++)
				{
					file.WriteLine($"{i}{i}{i}{i}{i}{i}{i}{i}");
					for (int j = 0; j < 10; j++)
						file.WriteLine(j);
				}
			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);
			using (var consoleOutput = new ConsoleOutput())
			{
				fileProcessor.Process();
				var data = consoleOutput.GetOutput();

				//Assert
				Assert.Equal(data, result);
			}
		}

		/// <summary>
		///  The second test generates random cusips and prices and verifies most of the result manually 
		/// </summary>
		[Fact]
		public void ProcessRandomBondsWithPrices()
		{
			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);
			var alphaRandom = new Random();
			var decimalRandom = new Random();

			using (var file = new StreamWriter(fqInputFile))
			{
				for (int i = 0; i < 100; i++)
				{
					file.WriteLine(alphaRandom.RandomString(8));
					for (int j = 0; j < 10; j++)
						file.WriteLine(decimalRandom.NextDouble(0.75, 100.0));
				}
			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);
			using (var consoleOutput = new ConsoleOutput())
			{
				fileProcessor.Process();
				var data = consoleOutput.GetOutput();

				//Assert
				Assert.False(data.Split('\n').Length == 100);
			}

		}

		/// <summary>
		/// The below test generates a large file and processes it... ~700M
		///  please uncomment to run it
		/// </summary>

		[Fact]
		public void ProcessLargeFiles()
		{
			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);
			var alphaRandom = new Random();
			var decimalRandom = new Random();

			using (var file = new StreamWriter(fqInputFile))
			{
				for (int i = 0; i < 100000; i++)
				{
					file.WriteLine(alphaRandom.RandomString(8));
					for (int j = 0; j < 1000; j++)
						file.WriteLine(decimalRandom.NextDouble(0.75, 100.0));
				}
			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);
			using (var consoleOutput = new ConsoleOutput())
			{
				fileProcessor.Process();
				var data = consoleOutput.GetOutput();

				//Assert
				Assert.False(data.Split('\n').Length == 100000000);
			}

		}

		[Fact]
		public void FailWhenInputFileHasNoValidCusipsAndPrices()
		{
			// Arrange a file with just dates
			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);

			var random = new Random();
			using (var file = new StreamWriter(fqInputFile))
			{
				for (int i = 0; i < 10; i++)
				{
					file.WriteLine(random.GetRandomDateTime(DateTime.Parse("Jan 1 2019"), DateTime.Parse("Jan 1 2019")));
				}
			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);

			Assert.Throws<ArgumentOutOfRangeException>(() => fileProcessor.Process());

		}

		[Fact]
		public void FailWhenInputFileIsMissing()
		{
			// Arrange a file with just dates
			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
			var inputFile = Path.Combine(binDirectory, PricingFile);

			if (File.Exists(inputFile))
				File.Delete(inputFile);

			Assert.Throws<FileNotFoundException>(() =>  ApplicationUtils.checkIfBondFileExists(inputFile));
		}


		[Fact]
		public void ProcessBondFileWithJustCusipsNoPrices()
		{
			// Arrange
			var result = @"Cusip=XXXXXXXX OpeningPrice=0 LowestPrice=0 HighestPrice=0 ClosingPrice=0
Cusip=YYYYYYYY OpeningPrice=0 LowestPrice=0 HighestPrice=0 ClosingPrice=0
Cusip=ZZZZZZZZ OpeningPrice=0 LowestPrice=0 HighestPrice=0 ClosingPrice=0
";


			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);

			using (var file = new StreamWriter(fqInputFile))
			{
				file.WriteLine("XXXXXXXX");
				file.WriteLine("YYYYYYYY");
				file.WriteLine("ZZZZZZZZ");
				
			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);
			using (var consoleOutput = new ConsoleOutput())
			{
				fileProcessor.Process();
				var data = consoleOutput.GetOutput();

				//Assert
				Assert.Equal(data, result);
			}
		}

		[Fact]
		public void ProcessBondFileWithNoCusipsJustPrices()
		{
			// Arrange
			var result = @"";


			var PricingFile = @"bondprices.data";
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var binDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

			var fqInputFile = Path.Combine(binDirectory, PricingFile);

			using (var file = new StreamWriter(fqInputFile))
			{
				file.WriteLine("100");
				file.WriteLine("200");
				file.WriteLine("300");

			}

			//Act
			var fileProcessor = new BondFileProcessor(fqInputFile);
			using (var consoleOutput = new ConsoleOutput())
			{
				fileProcessor.Process();
				var data = consoleOutput.GetOutput();

				//Assert
				Assert.Equal(data, result);
			}
		}

	}
}
