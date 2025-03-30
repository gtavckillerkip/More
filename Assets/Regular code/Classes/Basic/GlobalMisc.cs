namespace System.Runtime.CompilerServices
{
	public class IsExternalInit { }
}

namespace More.Basic
{
	public static class GlobalMisc
	{
		
	}

	public static class ResolutionIdentifierExtensions
	{
		public static string ToFriendlyString(this ResolutionIdentifier ri)
		{
			return ri switch
			{
				ResolutionIdentifier.HD =>			"HD",
				ResolutionIdentifier.FWXGA =>		"FWXGA",
				ResolutionIdentifier.FullHD =>		"Full HD",
				ResolutionIdentifier.HDPlus =>		"HD+",
				ResolutionIdentifier.QHD =>			"QHD",
				ResolutionIdentifier._4KUHD =>		"4K UHD",
				_ =>								""
			};
		}

		public static ResolutionIdentifier? FromFriendlyStringOrNull(string s)
		{
			return s switch
			{
				"HD" =>			ResolutionIdentifier.HD,
				"FWXGA" =>		ResolutionIdentifier.FWXGA,
				"Full HD" =>	ResolutionIdentifier.FullHD,
				"HD+" =>		ResolutionIdentifier.HDPlus,
				"QHD" =>		ResolutionIdentifier.QHD,
				"4K UHD" =>		ResolutionIdentifier._4KUHD,
				_ =>			null
			};
		}
	}

	public static class DifficultyExtensions
	{
		public static string ToFriendlyString(this Difficulty d)
		{
			return d switch
			{
				Difficulty.Low =>		"Low",
				Difficulty.Medium =>	"Medium",
				Difficulty.High =>		"High",
				_ => ""
			};
		}

		public static Difficulty? FromFriendlyStringOrNull(string s)
		{
			return s switch
			{
				"Low" =>		Difficulty.Low,
				"Medium" =>		Difficulty.Medium,
				"High" =>		Difficulty.High,
				_ => null
			};
		}
	}
}
