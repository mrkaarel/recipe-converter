using RecipeConverter.Model;
using System;
using System.Collections.Generic;

namespace RecipeConverter
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnConvert_Click(object sender, EventArgs e)
		{
			string input = txtRecipe.Text;

			var ruleSet = new List<ConversionRule>();
			ruleSet.Add(new ConversionRule() { SourceUnit = "pounds|pound|lbs|lb", TargetUnit = "kg", ConversionMethod = ConversionMethods.Factor(0.453592m) });
			ruleSet.Add(new ConversionRule() { SourceUnit = "ounces|ounce|oz", TargetUnit = "g", ConversionMethod = ConversionMethods.Factor(28.3495m), TargetFormatString = "#" });
			ruleSet.Add(new ConversionRule() { SourceUnit = "cups|cup", TargetUnit = "ml", ConversionMethod = ConversionMethods.Factor(236.588m), TargetFormatString = "#" });
			ruleSet.Add(new ConversionRule() { SourceUnit = "deg F|degrees F|° F|°F", TargetUnit = "C", ConversionMethod = (x) => (x - 32) / 1.8m, TargetFormatString = "#" });

			txtRecipe.Text = new Converter(ruleSet).Convert(input);
		}
	}
}