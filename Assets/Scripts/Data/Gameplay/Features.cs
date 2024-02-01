using Extensions;
using UnityEngine;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "Features", menuName = "Data/Gameplay/Features")]
	public sealed class Features : ScriptableObject
	{
        #region Fields

#if UNITY_EDITOR
        [ArrayElementTitle("_featureName")]
#endif
        [SerializeField] private FeatureData[] _features;

        #endregion


        #region Methods

        public FeatureData GetFeatureByName(FeaturesNames featureName)
        {
            return _features.FirstWhich(x => x.FeatureName == featureName);
        }

        #endregion
    }
}