using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace LandsHeart
{
    public static class SavingSystem
    {
        #region Constants

        private const string SAVE_DATA_KEY = "SaveData";

        [System.Serializable]
        private struct DataSaveList
        {
            [SerializeField] private List<SaveData> _saveDataList;
            public List<SaveData> SaveDataList => _saveDataList;

            public DataSaveList(List<SaveData> saveDataList)
            {
                _saveDataList = saveDataList.OrderBy(x => x.Time).ToList();
            }
        }

        #endregion


        #region Fields

        private static DataSaveList _currentSaveDatas;

        #endregion


        #region Properties

        public static SaveData CurrentSaveData { get; private set; }
        public static bool HasSaveData { get; private set; }

        #endregion


        #region Constructor

        static SavingSystem()
        {
            if(Application.isPlaying) LoadSaveDatas();
        }

        #endregion


        #region Methods

        public static void LoadSaveDatas()
        {
            if (PlayerPrefs.HasKey(SAVE_DATA_KEY))
            {
                _currentSaveDatas = JsonUtility.FromJson<DataSaveList>(PlayerPrefs.GetString(SAVE_DATA_KEY));
                HasSaveData = true;
                MessageLogger.Log($"Data was preloaded");
            }
            else
            {
                _currentSaveDatas = new DataSaveList(new List<SaveData>() { 
                    new SaveData(SceneStateMachine.Instance.CurrentSceneState.StateName)});
                HasSaveData = false;
                MessageLogger.Log("No data was preloaded");
            }
            CurrentSaveData = _currentSaveDatas.SaveDataList[_currentSaveDatas.SaveDataList.Count - 1];
        }

        private static void SaveSaveDatas()
        {
            var json = JsonUtility.ToJson(_currentSaveDatas);
            PlayerPrefs.SetString(SAVE_DATA_KEY, json);
            MessageLogger.Log($"Data was saved: {json}");
            LoadSaveDatas();
        }

        public static void LoadLastSaveData()
        {
            if(_currentSaveDatas.SaveDataList.Count > 0)
            {
                CurrentSaveData = _currentSaveDatas.SaveDataList[_currentSaveDatas.SaveDataList.Count - 1];
            }
            else
            {
                throw new System.IndexOutOfRangeException("There is no save to continue");
            }
        }

        public static void LoadSaveDataByIndex(byte index)
        {
            CurrentSaveData = index <= _currentSaveDatas.SaveDataList.Count ? _currentSaveDatas.SaveDataList[index] : 
                throw new System.IndexOutOfRangeException("Invalid index for save");
        }

        public static void SaveNewData(SaveData data)
        {
            MessageLogger.Log($"Saving new data: {JsonUtility.ToJson(data)}");
            _currentSaveDatas.SaveDataList.Add(data);
            SaveSaveDatas();
        }

        public static void RewriteSaveData(SaveData data, byte index)
        {
            MessageLogger.Log($"Rewriting data with index {index}");
            if(index <= _currentSaveDatas.SaveDataList.Count)
            {
                _currentSaveDatas.SaveDataList[index] = data;
                SaveSaveDatas();
            }
            else
            {
                throw new System.IndexOutOfRangeException("Invalid save index");
            }
        }

        public static void DeleteSaveData(byte index)
        {
            MessageLogger.Log($"Deleting data with index {index}");
            _currentSaveDatas.SaveDataList.RemoveAt(index);
            SaveSaveDatas();
        }

        public static void ClearSaveData()
        {
            _currentSaveDatas = default;
            PlayerPrefs.SetString(SAVE_DATA_KEY, string.Empty);
            PlayerPrefs.DeleteKey(SAVE_DATA_KEY);
            MessageLogger.Log("Save data cleared");
        }

        #endregion
    }
}

