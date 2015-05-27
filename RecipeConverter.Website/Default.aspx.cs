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
			ruleSet.Add(new ConversionRule(sourceUnit: "inches|inch", targetUnit: "cm", conversionFactor: 2.54m, formatString: AmountFormatStrings.Integer));
			ruleSet.Add(new ConversionRule(sourceUnit: "gallons|gallon|gals|gal", targetUnit: "l", conversionFactor: 3.78541m, formatString: AmountFormatStrings.OneDecimalPlace));
			ruleSet.Add(new ConversionRule(sourceUnit: "quarts|quart|qts|qt", targetUnit: "l", conversionFactor: 0.94635m, formatString: AmountFormatStrings.OneDecimalPlace));
			ruleSet.Add(new ConversionRule(sourceUnit: "pounds|pound|lbs|lb", targetUnit: "kg", conversionFactor: 0.453592m));
			ruleSet.Add(new ConversionRule(sourceUnit: "ounces|ounce|oz", targetUnit: "g", conversionFactor: 28.3495m, formatString: AmountFormatStrings.Integer));
			ruleSet.Add(new ConversionRule(sourceUnit: "cups|cup", targetUnit: "ml", conversionFactor: 236.588m, formatString: AmountFormatStrings.Integer));
			ruleSet.Add(new ConversionRule(sourceUnit: "deg F|degrees F|° F|°F", targetUnit: "°C", conversionMethod: (x) => (x - 32) / 1.8m, formatString: AmountFormatStrings.Integer));
			ruleSet.Add(new ConversionRule(sourceUnit: "degrees|deg|°", targetUnit: "°C", conversionMethod: (x) => (x - 32) / 1.8m, evaluator: ApplicabilityEvaluators.MinimumAmount(251), formatString: AmountFormatStrings.Integer));

			return new Converter(ruleSet).Convert(input);
		}
	}
}