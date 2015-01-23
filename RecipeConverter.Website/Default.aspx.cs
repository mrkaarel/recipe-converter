using RecipeConverter.Model;
using System;
using System.Collections.Generic;

namespace RecipeConverter
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(Request.Form["txtRecipe"])) {
				txtRecipe.Value = Convert(Request.Form["txtRecipe"]);
			}
		}

		private string Convert(string input)
		{
			var ruleSet = new List<ConversionRule>();
			ruleSet.Add(new ConversionRule() { SourceUnit = "pounds|pound|lbs|lb", TargetUnit = "kg", ConversionMethod = ConversionMethods.Factor(0.453592m) });
			ruleSet.Add(new ConversionRule() { SourceUnit = "ounces|ounce|oz", TargetUnit = "g", ConversionMethod = ConversionMethods.Factor(28.3495m), TargetFormatString = "#" });
			ruleSet.Add(new ConversionRule() { SourceUnit = "cups|cup", TargetUnit = "ml", ConversionMethod = ConversionMethods.Factor(236.588m), TargetFormatString = "#" });
			ruleSet.Add(new ConversionRule() { SourceUnit = "deg F|degrees F|° F|°F", TargetUnit = "C", ConversionMethod = (x) => (x - 32) / 1.8m, TargetFormatString = "#" });

			return new Converter(ruleSet).Convert(input);
		}
	}
}