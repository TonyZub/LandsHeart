using UnityEditor;
using UnityEngine;


namespace LandsHeart
{
	public sealed partial class Data
	{
        #region Constants

        private const string GAMEPLAY_SERVICES_DATAS_FOLDER_PATH = "GameplayServicesDatas/";

        #endregion


        #region Fields

        private static PhysicsServiceData _physicsServiceData;

        #endregion


        #region Properties

        public static PhysicsServiceData PhysicsServiceData => _physicsServiceData ??= Resources.
            Load<PhysicsServiceData>($"{GAMEPLAY_SERVICES_DATAS_FOLDER_PATH}{typeof(PhysicsServiceData).Name}");

        #endregion


        #region Editor extenstions
#if UNITY_EDITOR

        [MenuItem("Data/GameplayServices/PhysicsServiceData")]
        public static void SelectPhysicsService()
        {
            Selection.activeObject = PhysicsServiceData;
        }

#endif
        #endregion
    }
}