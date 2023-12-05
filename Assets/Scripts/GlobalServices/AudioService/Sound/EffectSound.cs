using System;
using UnityEngine;


namespace LandsHeart
{
	[Serializable]
	public sealed class EffectSound : Sound
	{
		#region Fields

		[SerializeField] private EffectSoundNames _soundName;

		#endregion


		#region Properties

		public EffectSoundNames SoundName => _soundName;

		#endregion
	}
}