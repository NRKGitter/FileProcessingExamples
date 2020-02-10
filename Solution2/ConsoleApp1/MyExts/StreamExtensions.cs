using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1.MyExts
{
	public static class StreamExtensions
	{
		/// <summary>
		/// lazy reading one line at a time
		/// </summary>
		/// <param name="reader"></param>
		/// <returns>IEnumerator<string></string></returns>
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
