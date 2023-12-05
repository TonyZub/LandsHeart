using UnityEngine;


namespace LandsHeart
{
	public sealed class AudioSourcePool : MonoBehaviour
	{
		#region Fields

		[SerializeField] private AudioSource _firstMusicSource;
		[SerializeField] private AudioSource _secondMusicSource;
		[Space]
		[SerializeField] private AudioSource _firstAmbienceSource;
		[SerializeField] private AudioSource _secondAmbienceSource;
		[Space]
		[SerializeField] private AudioSource[] _leftEffectSources;
		[SerializeField] private AudioSource[] _centerEffectSources;
		[SerializeField] private AudioSource[] _rightEffectSources;

		#endregion


		#region Properties

		public AudioSource FirstMusicSource => _firstMusicSource;
		public AudioSource SecondMusicSource => _secondMusicSource;
		public AudioSource FirstAmbienceSource => _firstAmbienceSource;
		public AudioSource SecondAmbienceSource => _secondAmbienceSource;
		public AudioSource[] LeftEffectSources => _leftEffectSources;
		public AudioSource[] CenterEffectSources => _centerEffectSources;
		public AudioSource[] RightEffectSources => _rightEffectSources;

        #endregion
    }
}