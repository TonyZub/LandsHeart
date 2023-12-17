#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace LandsHeart
{
	public sealed partial class Data
    {
        #region Constants

        private const string GLOBAL_SERVICE_DATAS_FOLDER_PATH = "GlobalServiceDatas/";

        #endregion


        #region Fields

        private static AudioServiceData _audioServiceData;
        private static CursorServiceData _cursorServiceData;
        private static LocalizationData _localizationData;

        #endregion


        #region Properties

        public static AudioServiceData AudioServiceData => _audioServiceData ??= Resources.
            Load<AudioServiceData>($"{GLOBAL_SERVICE_DATAS_FOLDER_PATH}{typeof(AudioServiceData).Name}");

        public static CursorServiceData CursorServiceData => _cursorServiceData ??= Resources.
            Load<CursorServiceData>($"{GLOBAL_SERVICE_DATAS_FOLDER_PATH}{typeof(CursorServiceData).Name}");

        public static LocalizationData LocalizationData => _localizationData ??= Resources.
            Load<LocalizationData>($"{GLOBAL_SERVICE_DATAS_FOLDER_PATH}{typeof(LocalizationData).Name}");

        #endregion


        #region Editor extenstions
#if UNITY_EDITOR

        [MenuItem("Data/GlobalServiceData/AudioServiceData")]
        public static void SelectAudioServiceData()
        {
            Selection.activeObject = AudioServiceData;
        }

        [MenuItem("Data/GlobalServiceData/CursorServiceData")]
        public static void SelectCursorServiceData()
        {
            Selection.activeObject = CursorServiceData;
        }

        [MenuItem("Data/LocalizationData")]
        public static void SelectLocalizationData()
        {
            Selection.activeObject = LocalizationData;
        }
#endif
        #endregion
    }
}