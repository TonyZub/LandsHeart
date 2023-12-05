using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


namespace EditorExtensions
{
	public sealed class SceneHelper
	{
		private static readonly string SCENES_PATH = "Assets/Scenes/";
		private static readonly string SCENE_EXTENSION = ".unity";
		private static readonly string START_SCENE_PATH_KEY = "StartScenePath";
		private static readonly string START_SCENE_INDEX_KEY = "StartSceneIndex";
        private static readonly string IS_BOOTSTRAP_IGNORED_KEY = "IsBootstrapIgnored";

        public static string CurrentSceneName => EditorSceneManager.GetActiveScene().name;
		public static int CurrentSceneIndex => EditorSceneManager.GetActiveScene().buildIndex;
		public static int StartSceneIndex => PlayerPrefs.GetInt(START_SCENE_INDEX_KEY);
		public static bool IsBootstrapIgnored => PlayerPrefs.GetInt(IS_BOOTSTRAP_IGNORED_KEY) == 1;

		public static void SetBootstrapIgnore(bool doIgnore)
		{
            PlayerPrefs.SetInt(IS_BOOTSTRAP_IGNORED_KEY, doIgnore ? 1 : 0);
        }

        public static void StartPlaymode()
		{
			if (IsBootstrapIgnored) return;
			RememberPreviousScene();
			OpenBootstrap();
		}

		public static void TrySetPreviousScene()
		{
            if (IsBootstrapIgnored) return;
            var previousScenePath = PlayerPrefs.GetString(START_SCENE_PATH_KEY);
			if (previousScenePath != string.Empty)
			{
				EditorSceneManager.OpenScene(previousScenePath);
				PlayerPrefs.SetString(START_SCENE_PATH_KEY, string.Empty);
			}
		}

		static void RememberPreviousScene()
		{
			PlayerPrefs.SetString(START_SCENE_PATH_KEY, GetScenePath(CurrentSceneName));
			PlayerPrefs.SetInt(START_SCENE_INDEX_KEY, CurrentSceneIndex);
		}

		public static bool SaveCurrentScene()
        {
			return EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
		}

		public static string GetScenePath(string sceneName)
        {
			return $"{SCENES_PATH}{sceneName}{SCENE_EXTENSION}";
        }

		public static void TryOpenScene(string sceneName)
        {
			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				EditorSceneManager.OpenScene(GetScenePath(sceneName));
			}
		}

		[MenuItem("Scenes/Bootstrap")] public static void OpenBootstrap() { TryOpenScene("Bootstrap"); }
		[MenuItem("Scenes/MainScene")] public static void OpenMainMenu(){ TryOpenScene("MainScene"); }
	}	
}
