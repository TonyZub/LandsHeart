using UnityEngine;
using UnityEditor;
using System.Net.Http;
using System.IO;
using System.Text;


namespace EditorExtensions
{
    public static class LocalesLoader
	{
        #region Constants

        public const string LOCALES_FILE_NAME = "Locales";
        public const string LOCALES_SHEET_URL = "https://docs.google.com/spreadsheets/d/1wKZx2NTL3YJHqVBWY-vJFiMGLdbexNawXdhFIuuOVG4/edit#gid=0";
        private static string LOCALES_FILE_PATH = "/Resources/Locales.json";
        private static string LOCALES_API_URL = "https://script.google.com/macros/s/AKfycbw7fStNxiGmc6QhvWiLpjhcvbhvKCdb1g5XXVVjA9P5RGMqzQNBcgHd1WV608Zs52TXzA/exec?request=locales";   

        #endregion


        #region Methods

        public static async void UpdateLocalLocales(bool doIgnoreMeta)
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync(LOCALES_API_URL);
            var parsedData = ParseJsonLocales(content);

            if(doIgnoreMeta || GetLocalesFromLocal().Meta != parsedData.Meta)
            {
                SaveLocalesToFile(parsedData);
                Debug.Log("JSON with locales was updated"); 
            }
        }

        private static LocalesData ParseJsonLocales(string json)
        {
            return JsonUtility.FromJson<LocalesData>(json);
        }

        private static async void SaveLocalesToFile(LocalesData data)
        {
            await File.WriteAllTextAsync(Application.dataPath + LOCALES_FILE_PATH, JsonUtility.ToJson(data), 
                Encoding.UTF8);
        }

        public static LocalesData GetLocalesFromLocal()
        {
            if (!File.Exists(Application.dataPath + LOCALES_FILE_PATH))
            {
                UpdateLocalLocales(true);
                return default;
            }
            var file = File.ReadAllText(Application.dataPath + LOCALES_FILE_PATH);
            return JsonUtility.FromJson<LocalesData>(file);
        }

        #endregion
    }
}