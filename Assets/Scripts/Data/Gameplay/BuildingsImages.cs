using UnityEngine;
using Extensions;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "BuildingsImages", menuName = "Data/Gameplay/BuildingsImages")]
	public sealed class BuildingsImages : ScriptableObject
	{
        #region PrivateData

        [System.Serializable]
        private struct BuildingImage
        {
            [SerializeField] private BuildingsNames _buildingName;
            [SerializeField] private Sprite _buildingSprite;
            [SerializeField] private GameObject _buildingPrefab;

            public BuildingsNames BuildingName => _buildingName;
            public Sprite BuildingSprite => _buildingSprite;
            public GameObject BuildingPrefab => _buildingPrefab;
        }

        #endregion


        #region Fields

#if UNITY_EDITOR
        [ArrayElementTitle("_buildingName")]
#endif
        [SerializeField] private BuildingImage[] _buildingNames;

        #endregion


        #region Methods

        public Sprite GetBuildingImage(BuildingsNames buildingName)
        {
            return _buildingNames.FirstWhich(x => x.BuildingName == buildingName).BuildingSprite;
        }

        #endregion
    }
}