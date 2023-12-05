using System;
using PixelCrushers.DialogueSystem;


namespace LandsHeart
{
    public sealed class LocalizationService
    {
        #region Events

        public event Action LanguageChanged;

        #endregion


        #region Properties

        public string[] CurrentLanguagesArray => LocalizationData.Instance.Languages;
        public int CurrentLanguageIndex => LocalesHelper.SelectedLocaleIndex;

        #endregion


        #region Constructor

        public LocalizationService()
        {
        }

        #endregion


        #region Methods

        public void SetLanguageIndex(int index)
        {
            LocalesHelper.SelectLocalePlaymode(index);
            UpdateDialogueSystemLanguage();
            LanguageChanged?.Invoke();
        }

        public void UpdateDialogueSystemLanguage()
        {
            if (DialogueManager.instance == null) return;
            MessageLogger.Log("set language to " + CurrentLanguagesArray[LocalesHelper.SelectedLocaleIndex]);
            DialogueManager.SetLanguage(CurrentLanguagesArray[LocalesHelper.SelectedLocaleIndex]);
        }

        #endregion
    }
}

