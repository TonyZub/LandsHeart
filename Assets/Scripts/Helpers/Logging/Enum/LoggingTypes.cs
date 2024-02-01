using System;


namespace LandsHeart
{
	[Flags]
	public enum LoggingTypes
	{
		None	= 0,
		Debug	= 1,
		Warning = 2,
		Error	= 4,
	}
}