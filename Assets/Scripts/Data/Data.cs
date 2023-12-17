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

        public static UIData UIData => _uiData ??= Resources.Load<UIData>(typeof(UIData).Name);

        #endregion


        #region Constructor

        private Data() { }

        #endregion


        #region Editor extenstions
#if UNITY_EDITOR

        [MenuItem("Data/DialogueDatabase")]
        public static void SelectDialogueDatabase()
        {
            Selection.activeObject = LocalizationData.DialogueDatabase;
        }
#endif
        #endregion
    }
}

