using UnityEngine;
using Extensions;


namespace LandsHeart
{
	[CreateAssetMenu(fileName = "Buildings", menuName = "Data/Gameplay/Buildings")]
	public sealed class Buildings : ScriptableObject
	{
        #region Fields

#if UNITY_EDITOR
        [ArrayElementTitle("_buildingName")]
#endif
        [SerializeField] private BuildingData[] _buildings;

        #endregion


        #region Methods

        public BuildingData GetBuildingByName(BuildingsNames buildingName)
        {
            return _buildings.FirstWhich(x => x.BuildingName == buildingName);
        }

        #endregion
    }
}