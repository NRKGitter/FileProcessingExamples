using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
	public struct ValueObject : IComparable<ValueObject>, IComparable
	{
		public Type typeOf { get; }
		public DateTime DateValue { get; }
		public Double DoubleValue { get; }
		public string StringValue { get; }

		public ValueObject(string data)			
		{
			this.DateValue = DateTime.MinValue;
			this.DoubleValue = Double.MinValue;
			this.StringValue = "";
			DateTime dateValue;
			Double doubleValue;
			bool isNumericValue = false;
			//if (DateTime.TryParseExact(data, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValueExact))
			//{

			//	//if (DateTime.TryParseExact(data, out dateValue))
			//	DateTime.TryParse(data, out dateValue);
			//	this.DateValue = dateValue;
			//	typeOf = typeof(System.DateTime);
			//}
			if (Regex.Match(data, @"^[0-9]*[.]?[0-9]*$").Success)
				isNumericValue = true;

			if (!isNumericValue && DateTime.TryParse(data, out dateValue))
			{
				this.DateValue = dateValue;
				typeOf = typeof(System.DateTime);
			}
			else if (isNumericValue && Double.TryParse(data, out doubleValue))
			{
				this.DoubleValue = doubleValue;
				typeOf = typeof(System.Double);
			}
			else
			{
				this.StringValue = data;
				typeOf = typeof(System.String);
			}
		}

		public int CompareTo(ValueObject other)
		{
			if (typeOf == typeof(System.DateTime))
			{
				return DateTime.Compare(this.DateValue, other.DateValue);
			}
			else if (typeOf == typeof(System.Double))
			{
				return this.DoubleValue.CompareTo(other.DoubleValue);
			}
			return String.CompareOrdinal(this.StringValue, other.StringValue);
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
				throw new ArgumentNullException("obj is null");
			if (!(obj is ValueObject))
				throw new ArgumentException("expected DataValue instance");
			return CompareTo((ValueObject)obj);
		}

		public override string ToString()
		{
			if (typeOf == typeof(System.DateTime))
				return DateValue.ToString();
			else if (typeOf == typeof(System.Double))
				return DoubleValue.ToString();
			return StringValue;
		}

	}
}
