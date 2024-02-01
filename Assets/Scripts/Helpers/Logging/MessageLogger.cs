using UnityEngine;


namespace LandsHeart
{
	public sealed class MessageLogger
	{
        #region Methods

        public static void Log(object message)
        {
			if (!Application.isPlaying || GlobalController.Instance.LoggingTypesEnabled.HasFlag(LoggingTypes.Debug))
			{
                Debug.Log(message);
            }
        }

		public static void LogWarning(object message)
        {
            if (!Application.isPlaying || GlobalController.Instance.LoggingTypesEnabled.HasFlag(LoggingTypes.Warning))
            {
                Debug.LogWarning(message);
            }
        }

		public static void LogError(object message)
        {
            if (!Application.isPlaying || GlobalController.Instance.LoggingTypesEnabled.HasFlag(LoggingTypes.Error))
            {
                Debug.LogError(message);
            }
        }

		#endregion
	}
}