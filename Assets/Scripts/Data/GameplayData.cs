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
        private static HumanStatusesColors _humanStatusesColors;
        private static Buildings _buildings;
        private static Features _features;
        private static Items _items;
        private static StartResourcesData _startResourcesData;

        #endregion


        #region Properties

        public static HumanImages HumanImages => _humanImages ??= Resources.
            Load<HumanImages>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(HumanImages).Name}");

        public static HumanStatusesColors HumanStatusesColors => _humanStatusesColors ??= Resources.
            Load<HumanStatusesColors>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(HumanStatusesColors).Name}");

        public static Buildings Buildings => _buildings ??= Resources.
            Load<Buildings>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(Buildings).Name}");

        public static Features Features => _features ??= Resources.
            Load<Features>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(Features).Name}");

        public static Items Items => _items ??= Resources.
            Load<Items>($"{GAMEPLAY_DATAS_FOLDER_PATH}{typeof(Items).Name}");

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

        [MenuItem("Data/Gameplay/HumanStatusesColors")]
        public static void SelectHumanStatusesColors()
        {
            Selection.activeObject = HumanStatusesColors;
        }

        [MenuItem("Data/Gameplay/Buildings")]
        public static void SelectBuildings()
        {
            Selection.activeObject = Buildings;
        }

        [MenuItem("Data/Gameplay/Features")]
        public static void SelectFeatures()
        {
            Selection.activeObject = Features;
        }

        [MenuItem("Data/Gameplay/Items")]
        public static void SelectItems()
        {
            Selection.activeObject = Items;
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