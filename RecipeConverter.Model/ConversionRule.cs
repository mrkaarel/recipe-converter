namespace RecipeConverter.Model
{
	public delegate decimal ConversionMethod(decimal input);

	public static class ConversionMethods
	{
		public static ConversionMethod Factor(decimal factor)
		{
			return (x) => x * factor;
		}
	}

	public class ConversionRule
	{
		public string SourceUnit { get; set; }
		public string TargetUnit { get; set; }
		public string TargetFormatString { get; set; }
		public ConversionMethod ConversionMethod { get; set; }

		public ConversionRule()
		{
			TargetFormatString = "0.##";
		}
	}
}
