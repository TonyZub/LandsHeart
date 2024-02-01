using System;
using UnityEngine;


namespace LandsHeart
{
    [Serializable]
	public struct BuildingData
	{
        #region Fields

        [SerializeField] private BuildingsNames _buildingName;
        [SerializeField] private Sprite _buildingSprite;
        [SerializeField] private GameObject _buildingPrefab;
        [SerializeField] private LocalizationDataHolder _buildingNameLocalized;
        [SerializeField] private LocalizationDataHolder _buildingDescription;

        #endregion


        #region Properties

        public BuildingsNames BuildingName => _buildingName;
        public Sprite BuildingSprite => _buildingSprite;
        public GameObject BuildingPrefab => _buildingPrefab;
        public LocalizationDataHolder BuildingNameLocalized => _buildingNameLocalized;
        public LocalizationDataHolder BuildingDescription => _buildingDescription;

        #endregion
    }
}