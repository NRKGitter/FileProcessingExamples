using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1.MyExts
{
	internal static class StringExtensions
	{

		/// <summary>
		/// checks for 8 character alphanumeric as valid cusips.....
		///  12345678 is a CUSIP but 1234567 and 123456789 are treated as prices...
		///  It is coded as an extension method but can(should) be a predicate.....
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static bool IsEightCharacterAlphaNumeric(this string data) => Regex.IsMatch(data, @"^\w{8}");
		private static Dictionary<string, BondPrice> BondPrices = new Dictionary<string, BondPrice>();

		/// <summary>
		/// build the dictionary from file stream iterator
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static void ProcessBondData(this IEnumerator<string> source)
		{
			decimal priceOrSymbol;
			decimal minCandidate = decimal.MaxValue, maxCandidate = 0, firstPrice = 0, lastPrice = 0;
			bool firstPriceFlag = false, atLeastOnePriceToProcess = false;
			string cusip = "";

			while (source.MoveNext())
			{
				var element = source.Current;
				var elementIsDecimal = Decimal.TryParse(element, out priceOrSymbol);

				if (!element.IsEightCharacterAlphaNumeric() && elementIsDecimal) // element is a price
				{
					atLeastOnePriceToProcess = true;
					if (firstPriceFlag)
					{
						firstPrice = priceOrSymbol;
						firstPriceFlag = false;
					}

					if (priceOrSymbol < minCandidate)
						minCandidate = priceOrSymbol;
					if (priceOrSymbol > maxCandidate)
						maxCandidate = priceOrSymbol;
					lastPrice = priceOrSymbol;

				}
				else if(element.IsEightCharacterAlphaNumeric())   // element is a Cusip
				{
					if (atLeastOnePriceToProcess && BondPrices.ContainsKey(cusip))
					{
						// update the key with key attribs
						UpdateCusip(cusip, minCandidate, maxCandidate, firstPrice, lastPrice);

						// initialize the key attribs
						minCandidate = decimal.MaxValue;
						maxCandidate = 0;
						lastPrice = 0;
						firstPrice = 0;
						atLeastOnePriceToProcess = false;

						Console.WriteLine(BondPrices[cusip].ToString()); // printing the output as soon as it is build

					}
					else if (!atLeastOnePriceToProcess && BondPrices.ContainsKey(cusip))
					{
						// update the key with zero prices
						UpdateCusipWithZeroPrices(cusip);

						// initialize the key attribs
						minCandidate = decimal.MaxValue;
						maxCandidate = 0;
						lastPrice = 0;
						firstPrice = 0;

						Console.WriteLine(BondPrices[cusip].ToString()); // printing the output as soon as it is build
					}
					// update the existing element ...
					cusip = element;
					if (!BondPrices.ContainsKey(cusip))
						BondPrices.Add(cusip, new BondPrice(element));
					firstPriceFlag = true;
				}
				else if (!element.IsEightCharacterAlphaNumeric() && !elementIsDecimal)
				{
					throw new ArgumentOutOfRangeException($"{element} was not a standard cusip value nor a decimal value");
				}
			}


			if (atLeastOnePriceToProcess && BondPrices.ContainsKey(cusip))
			{
				// update the key with key attribs
				UpdateCusip(cusip, minCandidate, maxCandidate, firstPrice, lastPrice);

				Console.WriteLine(BondPrices[cusip].ToString()); // printing the last bond

			}
			else if (!atLeastOnePriceToProcess && BondPrices.ContainsKey(cusip))
			{
				// update the key with zero prices
				UpdateCusipWithZeroPrices(cusip);

				Console.WriteLine(BondPrices[cusip].ToString()); // printing the last bond

			}
			//return result; // we don't need this dictionary...
		}

		private static void UpdateCusip(string cusip, decimal minCandidate, decimal maxCandidate, decimal firstPrice, decimal lastPrice)
		{
			BondPrices[cusip].MinPrice = minCandidate;
			BondPrices[cusip].MaxPrice = maxCandidate;
			BondPrices[cusip].EarlyPrice = firstPrice;
			BondPrices[cusip].LastPrice = lastPrice;
		}

		private static void UpdateCusipWithZeroPrices(string cusip)
		{
			BondPrices[cusip].MinPrice = 0;
			BondPrices[cusip].MaxPrice = 0;
			BondPrices[cusip].EarlyPrice = 0;
			BondPrices[cusip].LastPrice = 0;
		}
	}








}
