using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RecipeConverter.Model
{
    public class Converter
    {
		public IEnumerable<ConversionRule> RuleSet { get; set; }

		public Converter() : this(new List<ConversionRule>()) { }

		public Converter(IEnumerable<ConversionRule> ruleSet)
		{
			this.RuleSet = ruleSet;
		}

		public string Convert(string input)
		{
			return ConvertUnits(ConvertFractions(input));
		}

		public string ConvertUnits(string input)
		{
			string sourceUnits = String.Join("|", RuleSet.Select(r => r.SourceUnit));

			return Regex.Replace(input, @"(?<Amount>\d{1,3}(?:\.\d{1,3})?) ?(?<Unit>" + sourceUnits + @")", ConvertUnits);
		}

		public string ConvertFractions(string input)
		{
			string result = input;
			string sourceUnits = String.Join("|", RuleSet.Select(r => r.SourceUnit));

			result = Regex.Replace(result, @"(\d{1,5})?(¼|½|¾)(?= ?(" + sourceUnits + @"))", ConvertUnicodeVulgarFractionToAscii);
			result = Regex.Replace(result, @"((?:\d{1,5} +)?\d/\d)(?= ?(" + sourceUnits + @"))", ConvertAsciiVulgarFractionToDecimal);

			return result;
		}

		private static string ConvertUnicodeVulgarFractionToAscii(Match m)
		{
			string wholePart = m.Groups[1].Value;
			string fraction = m.Groups[2].Value;

			switch (fraction) {
				case "¼":
					fraction = "1/4";
					break;
				case "½":
					fraction = "1/2";
					break;
				case "¾":
					fraction = "3/4";
					break;
				default:
					break;
			}

			return !String.IsNullOrWhiteSpace(wholePart) ? String.Format("{0} {1}", wholePart, fraction) : fraction;
		}

		private static string ConvertAsciiVulgarFractionToDecimal(Match m)
		{
			var amountParts = m.Groups[1].Value.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			decimal resultAmount = 0;
			if (amountParts.Length > 1) {
				resultAmount = Int32.Parse(amountParts[0]);
			}

			var fractionalParts = amountParts.Last().Split('/');
			resultAmount += (decimal)Int32.Parse(fractionalParts[0]) / Int32.Parse(fractionalParts[1]);

			return resultAmount.ToString("0.##", CultureInfo.InvariantCulture);
		}

		private string ConvertUnits(Match m)
		{
			var rule = RuleSet.FirstOrDefault(r => r.IsMatchApplicable(m));
			if (rule == null) {
				return m.Value;
			}

			decimal amount = Decimal.Parse(m.Groups["Amount"].Value, CultureInfo.InvariantCulture);
			return String.Format("{0:" + rule.TargetFormatString + "} {1}", rule.ConversionMethod(amount), rule.TargetUnit);
		}
    }
}
