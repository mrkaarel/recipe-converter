namespace RecipeConverter.Model
{
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
}
