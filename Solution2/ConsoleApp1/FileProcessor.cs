using ConsoleApp1.MyExts;
using System.IO;

namespace ConsoleApp1
{
	public class BondFileProcessor
	{
		private string _FQPricingFile;
		public BondFileProcessor(string fileName)
		{
			this._FQPricingFile = fileName;
		}

		public void Process()
		{
			using (var reader1 = new StreamReader(_FQPricingFile))
			{
				var iterator = reader1.ToIterator();
				iterator.ProcessBondData();
			}

		}
	}
}