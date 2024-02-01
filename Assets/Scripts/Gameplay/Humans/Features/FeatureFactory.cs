using System;


namespace LandsHeart
{
	public sealed class FeatureFactory
	{
		#region Methods

		public Feature GetFeature(FeaturesNames featureName) => featureName switch
		{
			FeaturesNames.None => null,
			_ => throw new ArgumentException($"There is no feature with name {featureName}")
		};

		#endregion
	}
}