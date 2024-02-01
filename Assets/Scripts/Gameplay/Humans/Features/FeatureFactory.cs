using System;
using UnityEngine;


namespace LandsHeart
{
	public sealed class FeatureFactory
	{
        #region Methods

        public Feature CreateFeature(FeaturesNames featureName) => featureName switch
		{
			FeaturesNames.None => null,
			_ => throw new ArgumentException($"There is no feature with name {featureName}")
		};

		public FeatureCanvasModel CreateFeatureCanvasModel(Feature feature, Transform parent)
		{
			var newFeatureCanvasModel = GameObject.Instantiate(Data.UIData.FeatureCanvasPrefab, parent);
			newFeatureCanvasModel.SetData(feature);
			return newFeatureCanvasModel;
        }

		#endregion
	}
}