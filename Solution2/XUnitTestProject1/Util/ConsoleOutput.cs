using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XUnitTestProject1
{
	public class ConsoleOutput : IDisposable
	{
		private StringWriter stringWriter;
		private TextWriter originalOutput;

		public ConsoleOutput()
		{
			stringWriter = new StringWriter();
			this.originalOutput = Console.Out;
			Console.SetOut(stringWriter);
		}

		public string GetOutput() => stringWriter.ToString();
		public void Dispose()
		{
			Console.SetOut(originalOutput);
			stringWriter.Dispose();
		}

	}
}
