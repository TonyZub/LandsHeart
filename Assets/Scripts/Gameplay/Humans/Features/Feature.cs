using System;


namespace LandsHeart
{
	[Serializable]
	public abstract class Feature : ICycleEnd
	{
        #region Fields

        private readonly FeatureData _featureData;

        #endregion


        #region Properties

        public FeatureData FeatureData => _featureData;

        #endregion


        #region Constructor

        public Feature(FeatureData featureData)
        {
            _featureData = featureData;
        }

        #endregion


        #region ICycleEnd

        public virtual void OnCycleEnd()
        {
            MessageLogger.Log($"Feature {FeatureData.FeatureName} ended cycle");
            //TODO
        }

        #endregion
    }
}