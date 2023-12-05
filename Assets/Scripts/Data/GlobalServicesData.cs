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

        #endregion


        #region Properties

        public static AudioServiceData AudioServiceData
        {
            get
            {
                if (_audioServiceData == null)
                {
                    _audioServiceData = Resources.Load<AudioServiceData>(GLOBAL_SERVICE_DATAS_FOLDER_PATH +
                        typeof(AudioServiceData).Name);
                }
                return _audioServiceData;
            }
        }

        public static CursorServiceData CursorServiceData
        {
            get
            {
                if (_cursorServiceData == null)
                {
                    _cursorServiceData = Resources.Load<CursorServiceData>(GLOBAL_SERVICE_DATAS_FOLDER_PATH +
                        typeof(CursorServiceData).Name);
                }
                return _cursorServiceData;
            }
        }

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
#endif
        #endregion
    }
}