using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
	internal class BondPrice
	{
		public string Cusip { get; set; }

		public decimal EarlyPrice { get; set; }

		public decimal LastPrice { get; set; }

		public decimal MinPrice { get; set; }

		public decimal MaxPrice { get; set; }

		public BondPrice(string Cusip)
		{
			this.Cusip = Cusip;
		}

		public override string ToString()
		{
			return $"Cusip={Cusip} OpeningPrice={EarlyPrice} LowestPrice={MinPrice} HighestPrice={MaxPrice} ClosingPrice={LastPrice}";
		}

	}
}
