using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeConverter
{
	public partial class Default : System.Web.UI.Page
	{
		private List<ConversionRule> rules;

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string ConvertFractions(Match m)
		{
			var amountParts = m.Groups[1].Value.Split(' ');
			decimal resultAmount = 0;
			if (amountParts.Length > 1) {
				resultAmount = Int32.Parse(amountParts[0]);
			}

			var fractionalParts = amountParts.Last().Split('/');
			resultAmount += (decimal)Int32.Parse(fractionalParts[0]) / Int32.Parse(fractionalParts[1]);

			return resultAmount.ToString("0.##", CultureInfo.InvariantCulture);
		}

		public string ConvertUnits(Match m)
		{
			var rule = rules.First(u => u.SourceUnit.Contains(m.Groups[2].Value));
			decimal amount = Decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);

			return String.Format("{0:" + rule.TargetFormatString + "} {1}", amount * rule.Factor, rule.TargetUnit);
		}

		public class ConversionRule
		{
			public string SourceUnit { get; set; }
			public string TargetUnit { get; set; }
			public string TargetFormatString { get; set; }
			public decimal Factor { get; set; }

			public ConversionRule()
			{
				TargetFormatString = "0.##";
			}
		}

		protected void btnConvert_Click(object sender, EventArgs e)
		{
			string input = txtRecipe.Text;

			rules = new List<ConversionRule>();
			rules.Add(new ConversionRule() { SourceUnit = "pounds|pound|lbs|lb", TargetUnit = "kg", Factor = 0.453592m });
			rules.Add(new ConversionRule() { SourceUnit = "ounces|ounce|oz", TargetUnit = "g", Factor = 28.3495m, TargetFormatString = "#" });
			rules.Add(new ConversionRule() { SourceUnit = "cups|cup", TargetUnit = "ml", Factor = 236.588m, TargetFormatString = "#" });

			string result = input;
			string sourceUnits = String.Join("|", rules.Select(r => r.SourceUnit));
			result = Regex.Replace(result, @"((?:\d{1,5} )?\d/\d)(?= ?(" + sourceUnits + "))", ConvertFractions);
			result = Regex.Replace(result, @"(\d{1,3}(?:\.\d{1,3})?) ?(" + sourceUnits + @")\.?", ConvertUnits);

			txtRecipe.Text = result;
		}
	}
}