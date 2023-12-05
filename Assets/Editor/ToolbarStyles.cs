using UnityEngine;


namespace EditorExtensions
{
	static class ToolbarStyles
	{
		public static readonly GUIStyle topTooltipButtonStyle;
        public static readonly GUIStyle commandToggleStyle;

        static ToolbarStyles()
		{
			topTooltipButtonStyle = new GUIStyle("Command")
			{
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.TextOnly,
				fixedWidth = 100f,
				fixedHeight = 19f,
			};
            commandToggleStyle = new GUIStyle("Toggle")
            {
                alignment = TextAnchor.MiddleLeft,
                fixedWidth = 180f,
            };
        }
	}
}