using UnityEngine;
using System;
using System.IO;
using System.Linq;
using NaughtyAttributes;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;


namespace LandsHeart
{
    [CreateAssetMenu(fileName = "LocalizationData", menuName = "Data/LocalizationData")]
    public sealed class LocalizationData : ScriptableObject
    {
        #region Constants

        private const string LOCALES_FILE_PATH = "/Resources/Locales.json";

        #endregion


        #region Fields

        [SerializeField] private string[] _languages;
        [SerializeField] private DialogueDatabase _dialogueDatabase;
        [SerializeField] private List<SerializedLocaleData> _currentLocalesData;
        //private Dictionary<string, Dictionary<string, string[]>> _currentLocalesDict;
        private static LocalizationData _instance;

        #endregion


        #region Properties

        public string[] Languages => _languages;

        public DialogueDatabase DialogueDatabase => _dialogueDatabase;
        public List<SerializedLocaleData> CurrentLocalesData
        {
            get
            {
                if(_currentLocalesData == null || _currentLocalesData.Count == 0)
                {
                    UpdateLocalesDataFromJSON();
                }
                return _currentLocalesData;
            }
        }
        public static LocalizationData Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<LocalizationData>(typeof(LocalizationData).Name);
                return _instance;
            }
        }

        #endregion


        #region Methods

        [Button("Update locales")]
        public void UpdateLocalesDataFromJSON()
        {
            var parsedLocales = GetLocalesFromJSON();

            _currentLocalesData = new List<SerializedLocaleData>();
            foreach (var locale in parsedLocales.Data)
            {
                foreach (var item in locale.Locales)
                {
                    _currentLocalesData.
                        Add(new SerializedLocaleData(item.Key, locale.Part, item.Info, item.Rus, item.Eng, item.Chi));
                }
                
            }
        }

        public LocalesData GetLocalesFromJSON()
        {
            if (!File.Exists(Application.dataPath + LOCALES_FILE_PATH))
            {
                throw new Exception("Can't find JSON with locales, update locales!");
            }
            var file = File.ReadAllText(Application.dataPath + LOCALES_FILE_PATH);
            return JsonUtility.FromJson<LocalesData>(file);
        }

        public SerializedLocaleData GetLocaleByKey(string key)
        {
            return CurrentLocalesData.FirstOrDefault(x => x.Key == key);
        }

        public string[] GetLocaleParts()
        {
            return CurrentLocalesData.Select(x => x.Part).Distinct().ToArray();
        }

        public string[] GetLocaleKeys(string part)
        {
            return CurrentLocalesData.Where(x => x.Part.Equals(part)).Select(x => x.Key).ToArray();
        }

        public string[] GetLocaleKeysAll()
        {
            return CurrentLocalesData.Select(x => x.Key).ToArray();
        }

        public bool TryGetLocalizedValueByKey(string key, int languageIndex, out string localizedValue)
        {
            var locale = GetLocaleByKey(key);
            localizedValue = locale.LocalesArray[languageIndex];
            return !localizedValue.Equals(string.Empty);
        }

        public string GetLocalizedValueByKey(string key)
        {
            if(TryGetLocalizedValueByKey(key, LocalesHelper.SelectedLocaleIndex, out string localizedValue))
            {
                return localizedValue;
            }
            return string.Empty;
        }

        //[Button("Update locales in dialogues")]
        //private void UpdateLocalesInDialogues()
        //{
        //    try
        //    {
        //        _dialogueDatabase.ResetCache();
        //        var keys = GetLocaleKeysAll();
        //        foreach (var conversations in _dialogueDatabase.conversations)
        //        {
        //            foreach (var entry in conversations.dialogueEntries)
        //            {
        //                if (!entry.currentDialogueText.Equals(string.Empty) && keys.Contains(entry.currentDialogueText))
        //                {
        //                    var locale = CurrentLocalesData.Data.FirstOrDefault(x => x.Key == entry.currentDialogueText);
        //                    for (int i = 0; i < _languages.Length; i++)
        //                    {
        //                        entry.SetLocalizedDialogueText(_languages[i], locale.LocalesArray[i]);
        //                    }
        //                }
        //            }
        //        }
        //        Debug.Log("Locales in dialogues were updated");
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.LogError(e);
        //        Debug.LogError(e.StackTrace);
        //    }
        //}

        //private void SaveLocalesToDictionary()
        //{
        //    _currentLocalesDict = new Dictionary<string, Dictionary<string, string[]>>();
        //    var uniqueParts = _currentLocalesData.Data.Select(x => x.Part).Distinct();
        //    foreach (var part in uniqueParts)
        //    {
        //        if (!_currentLocalesDict.ContainsKey(part))
        //        {
        //            var embededDict = new Dictionary<string, string[]>();
        //            var elementsOfCurrentPart = _currentLocalesData.Data.Where(x => x.Part == part).ToArray();
        //            foreach (var element in elementsOfCurrentPart)
        //            {
        //                if (!embededDict.ContainsKey(element.Key))
        //                {
        //                    embededDict.Add(element.Key, element.LocalesArray);
        //                }
        //            }
        //        }
        //    }
        //}

        //public string GetStringByKey(string part, string key, int languageIndex)
        //{
        //    return _currentLocalesDict[part][key][languageIndex];
        //}

        #endregion
    }
}