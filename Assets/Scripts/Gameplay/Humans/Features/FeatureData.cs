using System;
using UnityEngine;


namespace LandsHeart
{
	[Serializable]
	public struct FeatureData
	{
		#region Fields

		[SerializeField] private FeaturesNames _featureName;
		[SerializeField] private Sprite _featureSprite;
		[SerializeField] private LocalizationDataHolder _featureNameLocalized;
		[SerializeField] private LocalizationDataHolder _featureDescription;

		#endregion


		#region Properties

		public FeaturesNames FeatureName => _featureName;
		public Sprite FeatureSprite => _featureSprite;
		public LocalizationDataHolder FeatureNameLocalized => _featureNameLocalized;
		public LocalizationDataHolder FeatureDescription => _featureDescription;

		#endregion
	}
}