using UnityEngine;


namespace LandsHeart
{
	public sealed class LocalesHelper
	{
		public static readonly string PLAYER_PREFS_SELECTED_LOCALE_KEY = "SelectedLocale";
		public static int SelectedLocaleIndex => PlayerPrefs.GetInt(PLAYER_PREFS_SELECTED_LOCALE_KEY);

		public static void SelectLocalePlaymode(int localeIndex)
        {
			SelectLocale(localeIndex);
		}

		private static void SelectLocale(int localeIndex)
        {
			PlayerPrefs.SetInt(PLAYER_PREFS_SELECTED_LOCALE_KEY, localeIndex);
			MessageLogger.Log($"Selected {LocalizationData.Instance.Languages[localeIndex]} language");
		}
	}
}