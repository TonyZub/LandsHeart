using System;
using UnityEngine;


namespace LandsHeart
{
	[Serializable]
	public sealed class AmbientSound : Sound
	{
		#region Fields

		[SerializeField] private AmbientSoundNames _soundName;

		#endregion


		#region Properties

		public AmbientSoundNames SoundName => _soundName;

		#endregion
	}
}