using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp1.Utils
{
	public class Util
	{

		public static class ApplicationUtils
		{
			public static void checkIfFileExists(string inputFile)
			{
				if (!File.Exists(inputFile))
				{
					throw new FileNotFoundException($"ERROR: file {inputFile} does not exist.");
				}
			}

		}

		public static IEnumerable<string> MergeSortedValueObjects(IEnumerator<string> iterator1, IEnumerator<string> iterator2)
		{
			var iterator1StillAvailable = iterator1.MoveNext();
			var iterator2StillAvailable = iterator2.MoveNext();

			while (iterator1StillAvailable && iterator2StillAvailable)
			{
				ValueObject v1 = new ValueObject(iterator1.Current);
				ValueObject v2 = new ValueObject(iterator2.Current);

				if (v1.typeOf != v2.typeOf)
				{
					throw new ArgumentException("Data types don't match for one of the values in the file");
				}

				if (v1.CompareTo(v2) < 1)
				{
					yield return v1.ToString();
					iterator1StillAvailable = iterator1.MoveNext();
				}
				else
				{
					yield return v2.ToString();
					iterator2StillAvailable = iterator2.MoveNext();
				}
			}

			//check which iterator can still provide values
			var iteratorRemaining = iterator1StillAvailable
			   ? iterator1
			   : iterator2StillAvailable ? iterator2 : null;

			if (null != iteratorRemaining)
			{
				do
				{
					yield return new ValueObject(iteratorRemaining.Current).ToString(); //iteratorRemaining.Current;
				} while (iteratorRemaining.MoveNext());
			}
		}
	}
}
