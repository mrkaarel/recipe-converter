using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeConverter.Model;
using System.Collections.Generic;

namespace RecipeConverter.Tests
{
	[TestClass]
	public class ConverterTests
	{
		[TestClass]
		public class ConvertMethod
		{
			[TestMethod]
			public void EmptyInput_ReturnsEmpty()
			{
				string input = "";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual("", converter.Convert(input));
			}

			[TestMethod]
			public void EmptyRuleSet_ReturnsOriginal()
			{
				string input = "3 foo";
				var converter = new Converter();

				Assert.AreEqual(input, converter.Convert(input));
			}

			[TestMethod]
			public void InputContainsVulgarFractions_UnitsAreConverted()
			{
				string input = "1/2 foo";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual(String.Format("{0} bar", 1.5m), converter.Convert(input));
			}

			[TestMethod]
			public void InputContainsUnitKeywordsWithoutUnits_KeywordsAreNotConverted()
			{
				string input = "many foo";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual(input, converter.Convert(input));
			}

			[TestMethod]
			public void InputContainsNumbersWithoutUnitKeywords_NumbersAreNotConverted()
			{
				string input = "1 boo 3/4 far";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual(input, converter.Convert(input));
			}
		}

		[TestClass]
		public class ConvertFractionsMethod
		{
			[TestMethod]
			public void InputContainsUnicodeFractionalUnits_UnitsConvertedToDecimal()
			{
				string input = "¼ foo";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual("0.25 foo", converter.ConvertFractions(input));
			}

			[TestMethod]
			public void InputContainsUnicodeFractionalUnitsWithWholePart_UnitsConvertedToDecimal()
			{
				string input = "1¼ foo";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual("1.25 foo", converter.ConvertFractions(input));
			}

			[TestMethod]
			public void InputContainsAsciiFractionalUnits_UnitsConvertedToDecimal()
			{
				string input = "1 1/2 foo";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual("1.5 foo", converter.ConvertFractions(input));
			}


			[TestMethod]
			public void InputContainsFractionsWithoutApplicableRules_FractionsNotConverted()
			{
				string input = "1 1/2 banana";
				var converter = new Converter(new List<ConversionRule>() { new ConversionRule("foo", "bar", 3) });

				Assert.AreEqual(input, converter.ConvertFractions(input));
			}
		}
	}
}
