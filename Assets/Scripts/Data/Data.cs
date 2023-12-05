#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace LandsHeart
{
    public sealed partial class Data
    {
        #region Fields

        private static Data _data;
        private static LocalizationData _localizationData;
        private static UIData _uiData;

        #endregion


        #region Properties

        public static Data Instance
        {
            get
            {
                if (_data == null)
                {
                    _data = new Data();
                }
                return _data;
            }
        }

        public static LocalizationData LocalizationData
        {
            get
            {
                if (_localizationData == null)
                {
                    _localizationData = Resources.Load<LocalizationData>(typeof(LocalizationData).Name);
                }
                return _localizationData;
            }
        }

        public static UIData UIData
        {
            get
            {
                if (_uiData == null)
                {
                    _uiData = Resources.Load<UIData>(typeof(UIData).Name);
                }
                return _uiData;
            }
        }

        #endregion


        #region Constructor

        private Data() { }

        #endregion


        #region Editor extenstions
#if UNITY_EDITOR

        [MenuItem("Data/LocalizationData")]
        public static void SelectLocalizationData()
        {
            Selection.activeObject = LocalizationData;
        }

        [MenuItem("Data/DialogueDatabase")]
        public static void SelectDialogueDatabase()
        {
            Selection.activeObject = LocalizationData.DialogueDatabase;
        }
#endif
        #endregion
    }
}

