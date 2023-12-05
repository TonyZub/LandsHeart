using UnityEditor;
using UnityEngine;


namespace EditorExtensions
{
	public sealed class LocalesHelperEditor
	{
		public static readonly string PLAYER_PREFS_SELECTED_LOCALE_KEY = "SelectedLocale";
		public static string[] LocalesArray => new string[3] { "ru", "en", "chi" };
		public static int SelectedLocaleIndex => PlayerPrefs.GetInt(PLAYER_PREFS_SELECTED_LOCALE_KEY);

		public static void SelectLocaleFromEditor(int localeIndex)
		{
			if (PlayerPrefs.HasKey(PLAYER_PREFS_SELECTED_LOCALE_KEY) &&
				(localeIndex == SelectedLocaleIndex || EditorApplication.isPlaying)) return;
			SelectLocale(localeIndex);
			//if (SceneHelper.SaveCurrentScene()) SceneHelper.ReloadCurrentScene();
		}

		private static void SelectLocale(int localeIndex)
        {
			PlayerPrefs.SetInt(PLAYER_PREFS_SELECTED_LOCALE_KEY, localeIndex);
			Debug.Log($"Selected {LocalesArray[localeIndex]} language");
		}
	}
}