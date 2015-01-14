using System;
using System.Text.RegularExpressions;

namespace OrderService
{
	public class OrderStateMapper : IMapper<string, OrderState?>
	{
		public OrderState? Map(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return null;

			string strRegex = @"(?<Filter>" +
							"\n" + @"     (?<Resource>.+?)\s+" +
							"\n" + @"     (?<Operator>eq|ne|gt|ge|lt|le|add|sub|mul|div|mod)\s+" +
							"\n" + @"     '?(?<Value>.+?)'?" +
							"\n" + @")" +
							"\n" + @"(?:" +
							"\n" + @"    \s*$" +
							"\n" + @"   |\s+(?:or|and|not)\s+" +
							"\n" + @")" +
							"\n";

			Regex myRegex = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

			var resource = myRegex.Replace(input, "${Resource}");
			var op = myRegex.Replace(input, "${Operator}");
			var value = myRegex.Replace(input, "${Value}");

			if (!string.Equals(resource, "State", StringComparison.InvariantCultureIgnoreCase) ||
				!string.Equals(op, "eq", StringComparison.InvariantCultureIgnoreCase))
				return null;

			OrderState enumValue;

			var valid = Enum.TryParse(value, true, out enumValue);

			if (!valid)
				return null;

			return enumValue;
		}
	}
}