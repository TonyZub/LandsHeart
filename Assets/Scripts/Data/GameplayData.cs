using UnityEditor;
using UnityEngine;


namespace LandsHeart
{
	public sealed partial class Data
	{
        #region Constants

        private const string GAMEPLAY_DATAS_FOLDER_PATH = "Gameplay/";

        #endregion


        #region Fields

        private static HumanImages _humanImages;
        private static BuildingsImages _buildingImages;
        private static StartResourcesData _startResourcesData;

        #endregion


        #region Properties

        public static HumanImages HumanImages => _humanImages ??= Resources.
            Load<HumanImages>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(HumanImages).Name}");

        public static BuildingsImages BuildingImages => _buildingImages ??= Resources.
            Load<BuildingsImages>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(BuildingsImages).Name}");

        public static StartResourcesData StartResourcesData => _startResourcesData ??= Resources.
            Load<StartResourcesData>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(StartResourcesData).Name}");

        #endregion


        #region Editor extenstions
#if UNITY_EDITOR

        [MenuItem("Data/Gameplay/HumanImages")]
        public static void SelectHumanImages()
        {
            Selection.activeObject = HumanImages;
        }

        [MenuItem("Data/Gameplay/BuildingImages")]
        public static void SelectBuildingImages()
        {
            Selection.activeObject = BuildingImages;
        }

        [MenuItem("Data/Gameplay/StartResourcesData")]
        public static void SelectStartResourcesData()
        {
            Selection.activeObject = StartResourcesData;
        }
#endif
        #endregion
    }
}