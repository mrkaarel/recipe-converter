using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace RecipeConverter.Model
{
	public static class ConversionMethods
	{
		public delegate decimal ConversionMethod(decimal input);

		public static ConversionMethod Factor(decimal factor)
		{
			return (x) => x * factor;
		}
	}

	public static class ApplicabilityEvaluators
	{
		public delegate bool ApplicabilityEvaluator(ConversionRule r, Match m);

		public static ApplicabilityEvaluator JustUnitMatch()
		{
			return (r, m) => r.SourceUnit.Split('|').Contains(m.Groups["Unit"].Value);
		}

		public static ApplicabilityEvaluator MinimumAmount(decimal amount)
		{
			return (r, m) => JustUnitMatch()(r, m) && decimal.Parse(m.Groups["Amount"].Value) >= amount;
		}
	}

	public static class AmountFormatStrings
	{
		public const string Integer = "#";
		public const string OneDecimalPlace = "0.#";
		public const string TwoDecimalPlaces = "0.##";
	}

	public class ConversionRule
	{
		public string SourceUnit { get; set; }
		public string TargetUnit { get; set; }
		public ConversionMethods.ConversionMethod ConversionMethod { get; set; }
		public ApplicabilityEvaluators.ApplicabilityEvaluator ApplicabilityEvaluator { get; set; }
		
		public string AmountFormatString { get; set; }

		public bool IsMatchApplicable(Match m)
		{
			return this.ApplicabilityEvaluator(this, m);
		}

		private ConversionRule()
		{

		}

		public ConversionRule(string sourceUnit, string targetUnit, decimal conversionFactor, string formatString = AmountFormatStrings.TwoDecimalPlaces) 
			: this(sourceUnit, targetUnit, ConversionMethods.Factor(conversionFactor), formatString)
		{}

		public ConversionRule(string sourceUnit, string targetUnit, ConversionMethods.ConversionMethod conversionMethod, string formatString = AmountFormatStrings.TwoDecimalPlaces)
			: this(sourceUnit, targetUnit, conversionMethod, ApplicabilityEvaluators.JustUnitMatch(), formatString)
		{}

		public ConversionRule(string sourceUnit, string targetUnit, ConversionMethods.ConversionMethod conversionMethod, ApplicabilityEvaluators.ApplicabilityEvaluator evaluator, string formatString = AmountFormatStrings.TwoDecimalPlaces)
			: this()
		{
			this.SourceUnit = sourceUnit;
			this.TargetUnit = targetUnit;
			this.ConversionMethod = conversionMethod;
			this.ApplicabilityEvaluator = evaluator;
			this.AmountFormatString = formatString;
		}
	}
}
