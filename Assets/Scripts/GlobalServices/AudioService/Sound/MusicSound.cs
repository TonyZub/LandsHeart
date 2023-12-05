using UnityEngine;
using System;


namespace LandsHeart
{
	[Serializable]
	public sealed class MusicSound : Sound
	{
		#region Fields

		[SerializeField] private MusicSoundNames _soundName;

		#endregion


		#region Properties

		public MusicSoundNames SoundName => _soundName;

		#endregion
	}
}