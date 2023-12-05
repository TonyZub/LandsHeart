using UnityEditor;
using UnityEngine;


namespace EditorExtensions
{
	[InitializeOnLoad]
	public sealed class EditorToolbarExtensionsController
	{
		static EditorToolbarExtensionsController()
		{
			if (Application.isPlaying) return;
			ToolbarExtender.LeftToolbarGUI.Add(OnLeftToolbarGUI);
			ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
			EditorApplication.playModeStateChanged += OnPlaymodeChanged;
		}

		static void OnLeftToolbarGUI()
		{
            GUILayout.FlexibleSpace();
            Update3DPartsVisibilityToggle();
        }

        static void OnRightToolbarGUI()
        {
			UpdateClearSaveButton();
			UpdateGetLocalesButton();
			UpdateGetLocalesSheetButton();
			UpdatLanguageChoiseButton();
			GUILayout.FlexibleSpace();
		}

        static void Update3DPartsVisibilityToggle()
        {
            SceneHelper.SetBootstrapIgnore(GUILayout.Toggle(SceneHelper.IsBootstrapIgnored,
				new GUIContent("IGNORE BOOTSTRAP", "Do start current scene directly"), ToolbarStyles.commandToggleStyle));
        }

        static void UpdateClearSaveButton()
		{
			if (GUILayout.Button(new GUIContent("CLEAR PREFS", "Clear player prefs"), ToolbarStyles.topTooltipButtonStyle))
			{
				PlayerPrefs.DeleteAll();
				LocalesHelperEditor.SelectLocaleFromEditor(0);
				Debug.Log("Player prefs were cleared");
			}
		}

		static void UpdateGetLocalesButton()
        {
			if (GUILayout.Button(new GUIContent("GET LOCALES", "Get locales from google sheet"), ToolbarStyles.topTooltipButtonStyle))
			{
				LocalesLoader.UpdateLocalLocales(true);
			}
		}

		static void UpdateGetLocalesSheetButton()
		{
			if (GUILayout.Button(new GUIContent("LOCALES", "Open locales google sheet"), ToolbarStyles.topTooltipButtonStyle))
			{
				Application.OpenURL(LocalesLoader.LOCALES_SHEET_URL);
			}
		}

		static void UpdatLanguageChoiseButton()
        {
			LocalesHelperEditor.SelectLocaleFromEditor(GUILayout.Toolbar(LocalesHelperEditor.SelectedLocaleIndex,
				LocalesHelperEditor.LocalesArray));
		}

		static void OnPlaymodeChanged(PlayModeStateChange currentState)
		{
            switch (currentState)
            {
                case PlayModeStateChange.EnteredEditMode:
					SceneHelper.TrySetPreviousScene();
					break;
                case PlayModeStateChange.ExitingEditMode:
					SceneHelper.StartPlaymode();
					break;
                case PlayModeStateChange.EnteredPlayMode:		
					break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
                default:
                    break;
            }		
		}
	}
}